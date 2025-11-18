using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
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
    public class AvatarSelectionViewModel : BaseViewModel
    {
        private readonly AvatarService _avatarService;
        private readonly INavigationService _navigationService;
        private readonly Page _view;

        public ObservableCollection<AvatarModel> CommonAvatars { get; } = new ObservableCollection<AvatarModel>();
        public ObservableCollection<AvatarModel> SpecialAvatars { get; } = new ObservableCollection<AvatarModel>();
        public ObservableCollection<AvatarModel> EpicAvatars { get; } = new ObservableCollection<AvatarModel>();
        public ObservableCollection<AvatarModel> LegendaryAvatars { get; } = new ObservableCollection<AvatarModel>();

        private AvatarModel _selectedAvatar;
        public AvatarModel SelectedAvatar
        {
            get => _selectedAvatar;
            set
            {
                if (SetProperty(ref _selectedAvatar, value) && value != null)
                {
                    UpdateSelectionStates(value);
                }
            }
        }

        public ICommand LoadAvatarsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectAvatarCommand { get; }

        public AvatarSelectionViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _avatarService = new AvatarService();

            LoadAvatarsCommand = new RelayCommand(async () => await LoadAvatarsAsync());
            SaveCommand = new RelayCommand(async () => await SaveAvatarAsync(), () => CanSave());
            CancelCommand = new RelayCommand(ExecuteCancel);
            SelectAvatarCommand = new RelayCommand<AvatarModel>(ExecuteSelectAvatar);
        }

        private async Task LoadAvatarsAsync()
        {
            SetLoading(true);
            try
            {
                var response = await _avatarService.GetAvatarsAsync(CurrentSession.CurrentUserNickname);
                var message = MessageTranslator.GetMessage(response.Code);
                if (!response.Success)
                {
                    _dialogService.ShowWarning(message);
                    return;
                }

                CommonAvatars.Clear();
                SpecialAvatars.Clear();
                EpicAvatars.Clear();
                LegendaryAvatars.Clear();

                foreach (var data in response.Data)
                {
                    var model = new AvatarModel
                    {
                        AvatarId = data.AvatarId,
                        Name = data.AvatarName,
                        Description = data.Description,
                        Rarity = data.Rarity,
                        IsUnlocked = data.IsUnlocked,
                        IsSelected = data.IsSelected
                    };

                    switch (model.Rarity?.ToLower() ?? "common")
                    {
                        case "special": SpecialAvatars.Add(model); break;
                        case "epic": EpicAvatars.Add(model); break;
                        case "legendary": LegendaryAvatars.Add(model); break;
                        case "common":
                        default: CommonAvatars.Add(model); break;
                    }

                    if (model.IsSelected)
                    {
                        SelectedAvatar = model;
                    }
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al cargar avatares: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al cargar avatares: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al cargar avatares: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al cargar avatares: {ex.Message}";
                HandleException(MessageCode.ProfileFetchFailed, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async Task SaveAvatarAsync()
        {
            if (SelectedAvatar == null)
            {
                return;
            }
            SetLoading(true);

            try
            {
                var response = await _avatarService.SetAvatarAsync(
                    CurrentSession.CurrentUserNickname,
                    SelectedAvatar.AvatarId
                );
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    if (CurrentSession.CurrentUserProfileData != null)
                    {
                        CurrentSession.CurrentUserProfileData.SelectedAvatarName = SelectedAvatar.Name;
                    }
                    _dialogService.ShowAlert(Global.SuccessLabel, message);
                    _navigationService.NavigateTo(new YourProfilePage());
                }
                else
                {
                    _dialogService.ShowWarning(message);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al actualizar el avatar: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al actualizar el avatar: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al actualizar el avatar: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al actualizar el avatar: {ex.Message}";
                HandleException(MessageCode.ProfileUpdateFailed, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void ExecuteCancel()
        {
            _navigationService.GoBack();
        }

        private void ExecuteSelectAvatar(AvatarModel avatar)
        {
            if (avatar == null)
            {
                return;
            }
            SelectedAvatar = avatar;
        }

        private bool CanSave()
        {
            return SelectedAvatar != null && SelectedAvatar.IsUnlocked;
        }

        private void UpdateSelectionStates(AvatarModel newlySelected)
        {
            foreach (var avatar in CommonAvatars.Concat(SpecialAvatars).Concat(EpicAvatars).Concat(LegendaryAvatars))
            {
                avatar.IsSelected = false;
            }
            newlySelected.IsSelected = true;
            (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (LoadAvatarsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (SaveCommand as  RelayCommand)?.RaiseCanExecuteChanged();
            (CancelCommand as  RelayCommand)?.RaiseCanExecuteChanged();
            (SelectAvatarCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (isLoading)
            {
                _dialogService.ShowLoading(_view);
            }
            else
            {
                _dialogService.HideLoading();
            }
        }
    }
}
