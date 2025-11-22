using System;
using System.Collections.Generic;
using System.Dynamic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Mappers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class ProfileViewViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProfileViewService _profileService;

        private bool _isGuest;
        public bool IsGuest
        {
            get => _isGuest;
            set => SetProperty(ref _isGuest, value);
        }

        private string _nickname = "...";
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _fullName = "...";
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email = "...";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private Uri _facebookUrl;
        public Uri FacebookUrl
        {
            get => _facebookUrl;
            set => SetProperty(ref _facebookUrl, value);
        }

        private Uri _instagramUrl;
        public Uri InstagramUrl
        {
            get => _instagramUrl;
            set => SetProperty(ref _instagramUrl, value);
        }

        private Uri _tikTokUrl;
        public Uri TikTokUrl 
        { 
            get => _tikTokUrl;
            set => SetProperty(ref _tikTokUrl, value);
        }

        private string _selectedAvatarImagePath;
        public string SelectedAvatarImagePath
        {
            get => _selectedAvatarImagePath;
            private set => SetProperty(ref _selectedAvatarImagePath, value);
        }

        private List<dynamic> _stats;
        public List<dynamic> Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        public ICommand LoadProfileCommand { get; }
        public ICommand ChangeAvatarCommand { get; }
        public ICommand ChangeDataCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand OpenSocialLinkCommand { get; }

        public ProfileViewViewModel(INavigationService navigationService, IDialogService dialogService, 
            IProfileViewService profileService) : base(dialogService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));

            LoadProfileCommand = new RelayCommand(async () => await LoadProfileData());
            ChangeAvatarCommand = new RelayCommand(ExecuteChangeAvatar, () => !IsGuest);
            ChangeDataCommand = new RelayCommand(ExecuteChangeData, () => !IsGuest);
            BackCommand = new RelayCommand(ExecuteGoBack);
            OpenSocialLinkCommand = new RelayCommand<string>(ExecuteOpenSocialLink);
        }

        private async Task LoadProfileData()
        {
            SetLoading(true);
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname) || IsGuestCheck())
                {
                    LoadDefaultData();
                    return;
                }
                var response = await _profileService.GetProfileDataAsync(CurrentSession.CurrentUserNickname);
                string message = MessageTranslator.GetMessage(response.Code);

                if (!response.Success || response.Data == null)
                {
                    _dialogService.ShowWarning(message);
                    return;
                }
                var clientProfile = ProfileDataMapper.ToClientModel(response.Data);
                PopulateViewModel(clientProfile);
                CurrentSession.CurrentUserProfileData = clientProfile;
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al cargar los datos del perfil: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al cargar los datos del perfil: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al cargar los datos del perfil: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al cargar los datos del perfil: {ex.Message}";
                HandleException(MessageCode.ProfileFetchFailed, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void ExecuteChangeAvatar()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new AvatarSelectionPage());
        }

        private void ExecuteChangeData()
        {
            SoundManager.PlayClick();
            if (CurrentSession.CurrentUserProfileData == null)
            {
                _dialogService.ShowWarning(ErrorMessages.ProfileNotLoadedMessageLabel);
                return;
            }
            _navigationService.NavigateTo(new EditProfilePage(CurrentSession.CurrentUserProfileData));
        }

        private void ExecuteGoBack()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new MainMenuPage());
        }

        private void ExecuteOpenSocialLink(string linkName)
        {
            SoundManager.PlayClick();
            if (CurrentSession.CurrentUserProfileData == null)
            {
                _dialogService.ShowWarning(ErrorMessages.ProfileNotLoadedMessageLabel);
                return;
            }

            string targetUrl = null;
            switch (linkName)
            {
                case "Facebook":
                    targetUrl = CurrentSession.CurrentUserProfileData.FacebookUrl;
                    break;
                case "Instagram":
                    targetUrl = CurrentSession.CurrentUserProfileData.InstagramUrl;
                    break;
                case "TikTok":
                    targetUrl = CurrentSession.CurrentUserProfileData.TikTokUrl;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(targetUrl))
            {
                BrowserHelper.OpenUrl(targetUrl);
            }
            else
            {
                _dialogService.ShowWarning(ErrorMessages.SocialNetworkNotConfiguredMessageLabel);
            }
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (ChangeAvatarCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ChangeDataCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (isLoading)
            {
                _dialogService.ShowLoading(null);
            }
            else
            {
                _dialogService.HideLoading();
            }
        }

        private bool IsGuestCheck()
        {
            IsGuest = string.Equals(CurrentSession.CurrentUserNickname, "Guest", StringComparison.OrdinalIgnoreCase);
            return IsGuest;
        }

        private void PopulateViewModel(ClientProfileData profileData)
        {
            Nickname = profileData.Nickname;
            FullName = profileData.FullName;
            Email = profileData.Email;
            string avatarName = string.IsNullOrEmpty(profileData.SelectedAvatarName) ? "LogoUNO" : profileData.SelectedAvatarName;
            SelectedAvatarImagePath = $"pack://application:,,,/Avatars/{avatarName}.png";
            FacebookUrl = CreateUri(profileData.FacebookUrl);
            InstagramUrl = CreateUri(profileData.InstagramUrl);
            TikTokUrl = CreateUri(profileData.TikTokUrl);
            Stats = CreateStatisticsList(profileData);
            IsGuestCheck();
        }

        private void LoadDefaultData()
        {
            var defaultData = new ClientProfileData
            {
                Nickname = "Guest",
                FullName = "-",
                Email = "-",
                SelectedAvatarName = "LogoUNO",
                MatchesPlayed = 0,
                Wins = 0,
                Losses = 0,
                ExperiencePoints = 0
            };
            PopulateViewModel(defaultData);
        }

        private static List<dynamic> CreateStatisticsList(ClientProfileData profileData)
        {
            string winRate = "0%";
            if (profileData.MatchesPlayed > 0)
            {
                double rate = (double)profileData.Wins / profileData.MatchesPlayed * 100;
                winRate = $"{(int)rate}%";
            }

            dynamic row = new ExpandoObject();
            row.MatchesPlayed = profileData.MatchesPlayed;
            row.Wins = profileData.Wins;
            row.Loses = profileData.Losses;
            row.GlobalPoints = profileData.ExperiencePoints;
            row.WinRate = winRate;

            return new List<dynamic> { row };
        }

        private static Uri CreateUri(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return null; //REVISAR LA FORMA DE CAMBIAR EL RETURN NULL POR ALGO MAS ADECUADO
            }
            return uri;
        }
    }
}
