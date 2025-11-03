using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        private readonly LoginService _authService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

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

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _authService = new LoginService();

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
                ErrorMessage = "◆ " + string.Join("\n◆ ", validationErrors);
                AlertManager.HandleWarning(ErrorMessage);
                return;
            }

            SetLoading(true);

            try
            {
                var response = await _authService.LoginAsync(credentials);
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    CurrentSession.CurrentUserNickname = this.Nickname;
                    _navigationService.NavigateTo(new MainMenuPage());
                }
                else
                {
                    ErrorMessage = message;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, ex);
            }
            catch (TimeoutException ex)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, ex);
            }
            catch (Exception ex)
            {
                LogManager.Error($"Fallo en inicio de sesión: {ex.Message}", ex);
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
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
                _dialogService.ShowLoading();
            }
            else 
            { 
                _dialogService.HideLoading(); 
            }
        }

        private void HandleException(string userMessage, Exception ex)
        {
            LogManager.Error($"Fallo en el inicio de sesión: {ex.Message}", ex);
            ErrorMessage = userMessage;
        }
    }
}
