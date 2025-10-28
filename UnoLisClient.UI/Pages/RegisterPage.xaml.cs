using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.UnoLisServerReference.Confirmation;
using UnoLisClient.UI.UnoLisServerReference.Register;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Utils;
using UnoLisClient.UI.Validators;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IRegisterManagerCallback, IConfirmationManagerCallback
    {
        private RegisterManagerClient _registerClient;
        private ConfirmationManagerClient _confirmationClient;
        private LoadingPopUpWindow _loadingPopUpWindow;
        private string _pendingEmail;

        public RegisterPage()
        {
            InitializeComponent();
            ResendVerificationCodeLabel.Visibility = Visibility.Collapsed;
        }

        public void ConfirmationResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    HandleConfirmationSuccess(message);
                }
                else
                {
                    ShowAlert(Global.UnsuccessfulLabel, message);
                }
            });
        }

        public void RegisterResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    HandleRegisterSuccess(message);
                }
                else
                {
                    ShowAlert(Global.UnsuccessfulLabel, message);
                }
            });
        }

        public void ResendCodeResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);
                string title = response.Success ? Global.SuccessLabel : Global.UnsuccessfulLabel;
                ShowAlert(title, message);
            });
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void ClickSignInButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            var registrationData = GetRegistrationDataFromUI();
            string rewritedPassword = RewritedPasswordField.Password;

            if (!ValidateRegistrationInput(registrationData, rewritedPassword))
            {
                return;
            }

            _pendingEmail = registrationData.Email;
            ExecuteRegister(registrationData);
        }

        private void SignUpLabelMouseDown(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            if (string.IsNullOrWhiteSpace(_pendingEmail))
            {
                return;
            }
            ExecuteResendCode(_pendingEmail);
        }

        private void ExecuteRegister(RegistrationData data)
        {
            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _registerClient = new RegisterManagerClient(context);
                _registerClient.Register(data);
            }
            catch (EndpointNotFoundException ex)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, ex);
            }
            catch (TimeoutException ex)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, ex);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
            }
        }

        private void ExecuteConfirmCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(_pendingEmail))
            {
                ShowAlert(Global.UnsuccessfulLabel, ErrorMessages.InvalidDataMessageLabel);
                return;
            }

            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _confirmationClient = new ConfirmationManagerClient(context);
                _confirmationClient.ConfirmCode(_pendingEmail, code);
            }
            catch (EndpointNotFoundException ex)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, ex);
            }
            catch (TimeoutException ex)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, ex);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
            }
        }

        private void ExecuteResendCode(string email)
        {
            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _confirmationClient = new ConfirmationManagerClient(context);
                _confirmationClient.ResendConfirmationCode(email);
            }
            catch (EndpointNotFoundException ex)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, ex);
            }
            catch (TimeoutException ex)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, ex);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
            }
        }

        private RegistrationData GetRegistrationDataFromUI()
        {
            return new RegistrationData
            {
                Nickname = NicknameTextBox.Text.Trim(),
                FullName = FullNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                Password = PasswordField.Password
            };
        }

        private bool ValidateRegistrationInput(RegistrationData data, string rewritedPassword)
        {
            List<string> errors = UserValidator.ValidateRegistration(data, rewritedPassword);
            if (errors.Count > 0)
            {
                string message = "◆ " + string.Join("\n◆ ", errors);
                ShowAlert(Global.UnsuccessfulLabel, message);
                return false;
            }
            return true;
        }

        private void HandleRegisterSuccess(string message)
        {
            ShowAlert(Global.SuccessLabel, message);

            var inputPopUp = new InputPopUpWindow(
                Global.ConfirmationLabel,
                Global.ConfirmationMessageLabel,
                Global.CodeLabel
            );
            inputPopUp.Owner = Window.GetWindow(this);

            if (inputPopUp.ShowDialog() == true)
            {
                string code = inputPopUp.UserInput;
                ExecuteConfirmCode(code);
            }

            ResendVerificationCodeLabel.Visibility = Visibility.Visible;
        }

        private void HandleConfirmationSuccess(string message)
        {
            ShowAlert(Global.SuccessLabel, message);
            NavigationService?.Navigate(new LoginPage());
        }

        private void HandleException(string userMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error($"Fallo en flujo de registro: {ex.Message}", ex);
            ShowAlert(Global.UnsuccessfulLabel, userMessage);
        }

        private void ShowAlert(string title, string message)
        {
            new SimplePopUpWindow(title, message).ShowDialog();
        }

        private void ShowLoading()
        {
            _loadingPopUpWindow = new LoadingPopUpWindow()
            {
                Owner = Window.GetWindow(this)
            };
            _loadingPopUpWindow.Show();
        }

        private void HideLoading()
        {
            _loadingPopUpWindow?.StopLoadingAndClose();
        }
    }
}
