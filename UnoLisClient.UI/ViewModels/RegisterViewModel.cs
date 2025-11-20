using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Register;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly RegisterService _registerService;
        private readonly ConfirmationCodeService _confirmationCodeService;
        private readonly INavigationService _navigationService;
        private readonly Page _view;

        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _rewritedPassword;
        public string RewritedPassword
        {
            get => _rewritedPassword;
            set => SetProperty(ref _rewritedPassword, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isResendVisible;
        public bool IsResendVisible
        {
            get => _isResendVisible;
            set => SetProperty(ref _isResendVisible, value);
        }

        private string _pendingEmail;

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ResendCodeCommand { get; }

        public RegisterViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _registerService = new RegisterService();
            _confirmationCodeService = new ConfirmationCodeService();

            RegisterCommand = new RelayCommand(async () => await ExecuteRegisterAsync());
            CancelCommand = new RelayCommand(ExecuteCancel, () => !IsLoading);
            ResendCodeCommand = new RelayCommand(async () => await ExecuteResendCodeAsync(), () => !IsLoading && _isResendVisible);
        }

        private async Task ExecuteRegisterAsync()
        {
            ErrorMessage = null;
            var registrationData = GetRegistrationDataFromUI();

            var validationErrors = UserValidator.ValidateRegistration(registrationData, RewritedPassword);
            if (validationErrors.Any())
            {
                _dialogService.HandleValidationErrors(validationErrors.ToList());
                return;
            }

            SetLoading(true);
            try
            {
                var response = await _registerService.RegisterAsync(registrationData);
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    _pendingEmail = this.Email;
                    IsResendVisible = true;
                    (ResendCodeCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    
                    await PromptForConfirmationCodeAsync();
                }
                else
                {
                    SetLoading(false);
                    _dialogService.ShowAlert(Global.UnsuccessfulLabel, message, PopUpIconType.Warning);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en registro de usuario: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en registro de usuario: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en registro de usuario: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en registro de usuario: {ex.Message}";
                HandleException(MessageCode.RegistrationInternalError, logMessage, ex);
            }
        }

        private async Task ExecuteConfirmCodeAsync(string code)
        {
            SetLoading(true);
            try
            {
                var response = await _confirmationCodeService.ConfirmCodeAsync(_pendingEmail, code);
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    _dialogService.ShowAlert(Global.SuccessLabel, message, PopUpIconType.Success);
                    _navigationService.NavigateTo(new LoginPage());
                }
                else
                {
                    _dialogService.ShowAlert(Global.UnsuccessfulLabel, message);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en confirmación de código: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en confirmación de código: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en confirmación de código: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en confirmación de código: {ex.Message}";
                HandleException(MessageCode.ConfirmationInternalError, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async Task ExecuteResendCodeAsync()
        {
            SetLoading(true);
            try
            {
                var response = await _confirmationCodeService.ResendCodeAsync(_pendingEmail);
                string message = MessageTranslator.GetMessage(response.Code);
                
                if (response.Success)
                {
                    await PromptForConfirmationCodeAsync();
                }
                else
                {
                    SetLoading(false);
                    _dialogService.ShowAlert(Global.UnsuccessfulLabel, message, PopUpIconType.Warning);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo en reenvío de código: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo en reenvío de código: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo en reenvío de código: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo en reenvío de código: {ex.Message}";
                HandleException(MessageCode.EmailSendingFailed, logMessage, ex);
            }
        }

        private void ExecuteCancel()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }

        private async Task PromptForConfirmationCodeAsync()
        {
            SetLoading(false);
            string code = _dialogService.ShowInputDialog(Global.ConfirmationLabel,
                        Global.ConfirmationMessageLabel,
                        Global.CodeLabel,
                        PopUpIconType.EmailVerification);
            if (!string.IsNullOrEmpty(code))
            {
                await ExecuteConfirmCodeAsync(code);
            }
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (RegisterCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (CancelCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ResendCodeCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (isLoading)
            {
                _dialogService.ShowLoading(_view);
            }
            else
            {
                _dialogService.HideLoading();
            }
        }

        private RegistrationData GetRegistrationDataFromUI()
        {
            return new RegistrationData
            {
                Nickname = this.Nickname ?? string.Empty,
                FullName = this.FullName ?? string.Empty,
                Email = this.Email ?? string.Empty,
                Password = this.Password ?? string.Empty
            };
        }
    }
}
