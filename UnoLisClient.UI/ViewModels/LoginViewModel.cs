using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginService _loginService;
        private readonly INavigationService _navigationService;

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
        : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _loginService = new LoginService();

            LoginCommand = new RelayCommand(async () => await ExecuteLoginAsync(), () => !IsLoading);
            CancelCommand = new RelayCommand(ExecuteCancel, () => !IsLoading);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister, () => !IsLoading);
        }

        private async Task ExecuteLoginAsync()
        {
            ErrorMessage = null;
            var credentials = new AuthCredentials { Nickname = this.Nickname.Trim(), Password = this.Password.Trim() };

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
                HandleLoginResponse(response);
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en inicio de sesión (Endpoint): {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en inicio de sesión (Timeout): {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en inicio de sesión (Communication): {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en inicio de sesión (Inesperado): {ex.Message}";
                HandleException(MessageCode.UnhandledException, logMessage, ex);
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

        private void HandleLoginResponse(ServiceResponse<object> response)
        {
            if (response.Success)
            {
                HandleLoginSuccessful(response);
            }
            else if (IsBanned(response.Code))
            {
                HandleBannedPlayer(response.Data);
            }
            else
            {
                string errorMessage = MessageTranslator.GetMessage(response.Code);
                ErrorMessage = errorMessage;
                _dialogService.ShowWarning(errorMessage);
            }
        }

        private void HandleLoginSuccessful(ServiceResponse<object> response)
        {
            CurrentSession.CurrentUserNickname = Nickname;
            try
            {
                ChatService.Instance.Initialize(Nickname);
                FriendsService.Instance.Initialize(Nickname);
                ReportSessionService.Instance.Initialize(Nickname);
            }
            catch (Exception chatEx)
            {
                string logMessage = $"Fallo al inicializar el servicio de chat: {chatEx.Message}";
                HandleException(MessageCode.UnhandledException, logMessage, chatEx);
            }

            string successMessage = string.Format(MessageTranslator.GetMessage(response.Code), Nickname);
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new MainMenuPage());
            _dialogService.ShowAlert(Global.SuccessLabel, successMessage, PopUpIconType.Success);
        }

        private void HandleBannedPlayer(object data)
        {
            BanInfo banInfo = data as BanInfo;
            if (banInfo == null || string.IsNullOrWhiteSpace(banInfo.FormattedTimeRemaining))
            {
                return;
            }
            string banMessage = string.Format(
                MessageTranslator.GetMessage(MessageCode.PlayerBanned),
                banInfo.FormattedTimeRemaining
            );
            _dialogService.ShowWarning(banMessage);
        }

        private bool IsBanned(MessageCode code)
        {
            if (code != MessageCode.PlayerBanned)
            {
                return false;
            }
            return true;
        }
    }
}
