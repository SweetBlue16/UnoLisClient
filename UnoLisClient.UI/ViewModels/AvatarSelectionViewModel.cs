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
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class AvatarModel : BaseViewModel
    {
        public int AvatarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public bool IsUnlocked { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string ImagePath
        {
            get
            {
                return $"pack://application:,,,/Avatars/{this.Name}.png";
            }
        }

        public string RarityBrush
        {
            get
            {
                switch (Rarity?.ToLower())
                {
                    case "special": return "LightGreen";
                    case "epic": return "DodgerBlue";
                    case "legendary": return "Gold";
                    default: return "#AAAAAA";
                }
            }
        }
    }

    public class AvatarSelectionViewModel : BaseViewModel
    {
        private readonly AvatarService _avatarService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, nameof(IsLoading));
        }

        public ICommand LoadAvatarsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectAvatarCommand { get; }

        public AvatarSelectionViewModel(Page view, IDialogService dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _dialogService = dialogService;
            _avatarService = new AvatarService();

            LoadAvatarsCommand = new RelayCommand(async () => await LoadAvatarsAsync());
            SaveCommand = new RelayCommand(async () => await SaveAvatarAsync(), () => CanSave());
            CancelCommand = new RelayCommand(ExecuteCancel);
            SelectAvatarCommand = new RelayCommand<AvatarModel>(ExecuteSelectAvatar);
        }

        private async Task LoadAvatarsAsync()
        {
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
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al cargar avatares: {timeoutEx.Message}";
                HandleException(ErrorMessages.TimeoutMessageLabel, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al cargar avatares: {commEx.Message}";
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al cargar avatares: {ex.Message}";
                HandleException(ErrorMessages.UnknownErrorMessageLabel, logMessage, ex);
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
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al actualizar el avatar: {timeoutEx.Message}";
                HandleException(ErrorMessages.TimeoutMessageLabel, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al actualizar el avatar: {commEx.Message}";
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al actualizar el avatar: {ex.Message}";
                HandleException(ErrorMessages.UnknownErrorMessageLabel, logMessage, ex);
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

        private void HandleException(string userMessage, string logMessage, Exception ex)
        {
            SetLoading(false);
            LogManager.Error(logMessage, ex);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                _dialogService.ShowAlert(Global.UnsuccessfulLabel, userMessage);
            }));
        }
    }
}
