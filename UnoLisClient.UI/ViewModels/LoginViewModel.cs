using System;
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
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginService _loginService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private readonly Page _view;

        private string _nickname = "";
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, nameof(IsLoading));
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(IDialogService dialogService, Page view)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _dialogService = dialogService;
            _loginService = new LoginService();

            LoginCommand = new RelayCommand(async () => await ExecuteLoginAsync(), () => !IsLoading);
            CancelCommand = new RelayCommand(ExecuteCancel, () => !IsLoading);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister, () => !IsLoading);
        }

        private async Task ExecuteLoginAsync()
        {
            ErrorMessage = null;
            var credentials = new AuthCredentials { Nickname = this.Nickname, Password = this.Password };

            var validationErrors = UserValidator.ValidateLogin(credentials);
            if (validationErrors.Any())
            {
                _dialogService.HandleValidationErrors(validationErrors.ToList());
                return;
            }

            SetLoading(true);
            try
            {
                var response = await _loginService.LoginAsync(credentials);
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    CurrentSession.CurrentUserNickname = Nickname;
                    string successMessage = string.Format(message, Nickname);
                    SoundManager.PlayClick();
                    _navigationService.NavigateTo(new MainMenuPage());
                    _dialogService.ShowAlert(Global.SuccessLabel, successMessage);
                }
                else
                {
                    ErrorMessage = message;
                    _dialogService.ShowWarning(ErrorMessage);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en inicio de sesión: {enfEx.Message}";
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en inicio de sesión: {timeoutEx.Message}";
                HandleException(ErrorMessages.TimeoutMessageLabel, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en inicio de sesión: {commEx.Message}";
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en inicio de sesión: {ex.Message}";
                HandleException(ErrorMessages.UnknownErrorMessageLabel, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void ExecuteCancel()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }

        private void ExecuteGoToRegister()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new RegisterPage());
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (CancelCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoToRegisterCommand as RelayCommand)?.RaiseCanExecuteChanged();

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
