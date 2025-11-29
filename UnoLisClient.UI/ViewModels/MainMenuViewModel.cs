using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
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
    public class MainMenuViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly LogoutService _logoutService;
        private readonly Page _view;

        public ICommand PlayCommand { get; }
        public ICommand GoToSettingsCommand { get; }
        public ICommand GoToShopCommand { get; }
        public ICommand GoToProfileCommand { get; }
        public ICommand GoToLeaderboardsCommand { get; }
        public ICommand GoToFriendsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand GoToHowToPlayCommand { get; }

        public MainMenuViewModel(Page view, IDialogService dialogService): base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _logoutService = new LogoutService();

            PlayCommand = new RelayCommand(ExecuteGoToPlay, () => !IsLoading);
            GoToSettingsCommand = new RelayCommand(ExecuteGoToSettings, () => !IsLoading);
            GoToShopCommand = new RelayCommand(ExecuteGoToShop, () => !IsLoading);
            GoToProfileCommand = new RelayCommand(ExecuteGoToProfile, () => !IsLoading);
            GoToLeaderboardsCommand = new RelayCommand(ExecuteGoToLeaderboards, () => !IsLoading);
            GoToFriendsCommand = new RelayCommand(ExecuteGoToFriends, () => !IsLoading);
            LogoutCommand = new RelayCommand(async () => await ExecuteLogoutAsync(), () => !IsLoading);
            GoToHowToPlayCommand = new RelayCommand(ExecuteGoToHowToPlay, () => !IsLoading);
        }

        private async Task ExecuteLogoutAsync()
        {
            SoundManager.PlayClick();
            bool confirm = _dialogService.ShowQuestionDialog(
                Global.ConfirmationLabel,
                Global.LogoutMessageLabel,
                PopUpIconType.Logout
            );

            if (!confirm)
            {
                return;
            }

            if (IsGuest())
            {
                ClearLocalSessionAndNavigate();
                return;
            }

            SetLoading(true);
            try
            {
                var response = await _logoutService.LogoutAsync(CurrentSession.CurrentUserNickname);
                string userMessage = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    _dialogService.ShowAlert(Global.SuccessLabel, userMessage, PopUpIconType.Success);
                    ChatService.Instance.Cleanup();
                    FriendsService.Instance.Cleanup();
                    ClearLocalSessionAndNavigate();
                }
                else
                {
                    _dialogService.ShowWarning(userMessage);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
                return;
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
                return;
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
                return;
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en cierre de sesión: {ex.Message}";
                HandleException(MessageCode.LogoutInternalError, logMessage, ex);
                return;
            }
            finally
            {
                SetLoading(false);
            }
            ClearLocalSessionAndNavigate();
        }

        private void ExecuteGoToPlay()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new MatchMenuPage());
        }

        private void ExecuteGoToSettings()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new SettingsPage());
        }

        private void ExecuteGoToShop()
        {
            SoundManager.PlayClick();
            if (IsGuest())
            {
                _dialogService.ShowWarning(ErrorMessages.OperationNotSupportedMessageLabel);
                return;
            }
            _navigationService.NavigateTo(new AvatarShopPage());
        }

        private void ExecuteGoToProfile()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new YourProfilePage());
        }

        private void ExecuteGoToLeaderboards()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new LeaderboardsPage());
        }

        private void ExecuteGoToFriends()
        {
            SoundManager.PlayClick();
            if (IsGuest())
            {
                _dialogService.ShowWarning(ErrorMessages.OperationNotSupportedMessageLabel);
                return;
            }
            _navigationService.NavigateTo(new FriendsPage());
        }

        private void ExecuteGoToHowToPlay()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new HowToPlayPage());
        }

        private void ClearLocalSessionAndNavigate()
        {
            CurrentSession.CurrentUserNickname = null;
            CurrentSession.CurrentUserProfileData = null;
            _navigationService.NavigateTo(new GamePage());
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (PlayCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToSettingsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToShopCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToProfileCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToLeaderboardsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToFriendsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (LogoutCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToHowToPlayCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (isLoading)
            {
                _dialogService.ShowLoading(_view);
            }
            else
            {
                _dialogService.HideLoading();
            }
        }

        private static bool IsGuest()
        {
            return CurrentSession.CurrentUserNickname.Contains("Guest"); ;
        }
    }
}
