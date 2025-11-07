using System;
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
    public class MainMenuViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly LogoutService _logoutService;
        private readonly Page _view;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand PlayCommand { get; }
        public ICommand GoToSettingsCommand { get; }
        public ICommand GoToShopCommand { get; }
        public ICommand GoToProfileCommand { get; }
        public ICommand GoToLeaderboardsCommand { get; }
        public ICommand GoToFriendsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand GoToHowToPlayCommand { get; }

        public MainMenuViewModel(Page view, IDialogService dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _dialogService = dialogService;
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
                Global.LogoutMessageLabel
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

            _dialogService.ShowLoading(_view);
            string userMessage = string.Empty;
            bool success = false;

            try
            {
                var response = await _logoutService.LogoutAsync(CurrentSession.CurrentUserNickname);
                userMessage = MessageTranslator.GetMessage(response.Code);
                success = response.Success;
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {enfEx.Message}";
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {timeoutEx.Message}";
                HandleException(ErrorMessages.TimeoutMessageLabel, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en cierre de sesión: {commEx.Message}";
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en cierre de sesión: {ex.Message}";
                HandleException(ErrorMessages.UnknownErrorMessageLabel, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }

            _dialogService.ShowAlert(
                success ? Global.SuccessLabel : Global.OopsLabel,
                userMessage
            );
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

        private void HandleException(string userMessage, string logMessage, Exception ex)
        {
            _dialogService.HideLoading();
            LogManager.Error(logMessage, ex);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                _dialogService.ShowAlert(Global.UnsuccessfulLabel, userMessage);
            }));
        }

        private bool IsGuest()
        {
            return string.Equals(CurrentSession.CurrentUserNickname, "Guest", StringComparison.OrdinalIgnoreCase);
        }
    }
}
