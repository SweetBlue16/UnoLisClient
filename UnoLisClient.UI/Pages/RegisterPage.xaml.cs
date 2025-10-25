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
                _loadingPopUpWindow?.StopLoadingAndClose();
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                    NavigationService?.Navigate(new LoginPage());
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        public void RegisterResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                    var inputPopUp = new InputPopUpWindow(
                        Global.ConfirmationLabel,
                        Global.ConfirmationMessageLabel,
                        Global.CodeLabel
                    );
                    inputPopUp.Owner = Window.GetWindow(this);

                    if (inputPopUp.ShowDialog() == true)
                    {
                        string code = inputPopUp.UserInput;
                        CallConfirmationService(code);
                    }
                    ResendVerificationCodeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        public void ResendCodeResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                string message = MessageTranslator.GetMessage(response.Code);
                string title = response.Success ? Global.SuccessLabel : Global.UnsuccessfulLabel;
                new SimplePopUpWindow(title, message).ShowDialog();
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
            string nickname = NicknameTextBox.Text.Trim();
            string fullname = FullNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordField.Password;
            string rewritedPassword = RewritedPasswordField.Password;

            var registrationData = new RegistrationData
            {
                Nickname = nickname,
                FullName = fullname,
                Email = email,
                Password = password
            };

            List<string> errors = UserValidator.ValidateRegistration(registrationData, rewritedPassword);
            if (errors.Count > 0)
            {
                string message = "◆ " + string.Join("\n◆ ", errors);
                new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                return;
            }

            _pendingEmail = email;
            try
            {
                _loadingPopUpWindow = new LoadingPopUpWindow()
                {
                    Owner = Window.GetWindow(this)
                };
                _loadingPopUpWindow.Show();
                var context = new InstanceContext(this);
                _registerClient = new RegisterManagerClient(context);
                _registerClient.Register(registrationData);
            }
            catch (Exception)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
            }
        }

        private void SignUpLabelMouseDown(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            if (string.IsNullOrWhiteSpace(_pendingEmail))
            {
                return;
            }

            try
            {
                _loadingPopUpWindow = new LoadingPopUpWindow()
                {
                    Owner = Window.GetWindow(this)
                };
                _loadingPopUpWindow.Show();

                var context = new InstanceContext(this);
                _confirmationClient = new ConfirmationManagerClient(context);
                _confirmationClient.ResendConfirmationCode(_pendingEmail);
            }
            catch (Exception)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
            }
        }

        private void CallConfirmationService(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(_pendingEmail))
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.InvalidDataMessageLabel).ShowDialog();
                return;
            }

            try
            {
                _loadingPopUpWindow = new LoadingPopUpWindow()
                {
                    Owner = Window.GetWindow(this)
                };
                _loadingPopUpWindow.Show();

                var context = new InstanceContext(this);
                _confirmationClient = new ConfirmationManagerClient(context);
                _confirmationClient.ConfirmCode(_pendingEmail, code);
            }
            catch (Exception)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
            }
        }
    }
}
