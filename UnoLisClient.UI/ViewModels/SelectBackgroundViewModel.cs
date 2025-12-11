using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class SelectBackgroundViewModel : BaseViewModel
    {
        private const string Fei = "FEI";
        private const string Anfiteatro = "Anfiteatro";
        private const string Canchas = "Canchas";
        private const string Banca = "Banca";

        private const string FeiVideo = "FeiVideo.mp4"; 
        private const string AnfiteatroVideo = "AnfiteatroVideo.mp4";
        private const string CanchaVideo = "CanchaVideo.mp4";
        private const string BancaVideo = "BancaVideo.mp4";

        private const string FeiImage = "/Assets/FeiImage.png";
        private const string AnfiteatroImage = "/Assets/AnfiteatroImage.png";
        private const string CanchasImage = "/Assets/CanchasImage.png";
        private const string BancaImage = "/Assets/BancaImage.png";

        private readonly INavigationService _navigationService;
        private readonly IMatchmakingService _matchmakingService;
        private readonly string _lobbyCode;

        public ObservableCollection<BackgroundItemViewModel> Backgrounds { get; } = new ObservableCollection<BackgroundItemViewModel>();

        private BackgroundItemViewModel _selectedBackground;
        public BackgroundItemViewModel SelectedBackground
        {
            get => _selectedBackground;
            set
            {
                if (SetProperty(ref _selectedBackground, value))
                {
                    foreach (var background in Backgrounds)
                    {
                        background.IsSelected = (background == value);
                    }
                    (ConfirmCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SelectBackgroundCommand { get; }
        public ICommand ConfirmCommand { get; }

        public SelectBackgroundViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            string lobbyCode)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _matchmakingService = MatchmakingService.Instance;
            _lobbyCode = lobbyCode;

            SelectBackgroundCommand = new RelayCommandGeneric<BackgroundItemViewModel>(ExecuteSelectBackground);

            ConfirmCommand = new RelayCommand(async () => await ExecuteConfirmAsync(), () => SelectedBackground != null && !IsLoading);
        }

        public void LoadBackgrounds()
        {
            try
            {
                Backgrounds.Clear();

                Backgrounds.Add(new BackgroundItemViewModel(Fei, FeiImage, FeiVideo));
                Backgrounds.Add(new BackgroundItemViewModel(Anfiteatro, AnfiteatroImage, AnfiteatroVideo));
                Backgrounds.Add(new BackgroundItemViewModel(Canchas, CanchasImage, CanchaVideo));
                Backgrounds.Add(new BackgroundItemViewModel(Banca, BancaImage, BancaVideo));

                if (Backgrounds.Any())
                {
                    SelectedBackground = Backgrounds.First();
                }
                else
                {
                    throw new InvalidOperationException("No background assets were loaded.");
                }
            }
            catch (ArgumentNullException ex)
            {
                HandleException(MessageCode.UnhandledException, "Error: Resource path is null.", ex);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.UnhandledException, "Unexpected error initializing background selection.", ex);
            }
        }

        private void ExecuteSelectBackground(BackgroundItemViewModel item)
        {
            if (item == null)
            {
                return;
            }
            SoundManager.PlayClick();
            SelectedBackground = item;
        }

        private async Task ExecuteConfirmAsync()
        {
            if (SelectedBackground == null)
            {
                return;
            }

            IsLoading = true;
            SoundManager.PlayClick();

            try
            {
                bool success = await _matchmakingService.SetLobbyBackgroundAsync(_lobbyCode, SelectedBackground.VideoName);

                if (success)
                {
                    _dialogService.ShowAlert(Lobby.LobbyCreatedLabel, string.Format(Lobby.LobbyCreatedMessageLabel, _lobbyCode), PopUpIconType.Success);
                    _navigationService.NavigateTo(new MatchLobbyPage(_lobbyCode));
                }
                else
                {
                    _dialogService.ShowWarning(Lobby.CouldNotSetBackgroundMessageLabel);
                }
            }
            catch (TimeoutException ex)
            {
                HandleException(MessageCode.Timeout, "Server request timed out.", ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(MessageCode.ConnectionFailed, "Network error communicating with server.", ex);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ConfigurationError, "Unexpected error setting background.", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}