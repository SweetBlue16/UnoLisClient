using System;
using System.Collections.Generic;
using System.Linq;
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
using UnoLisClient.UI.UnoLisServerReference.Login;
using UnoLisClient.UI.Validators;
using System.ServiceModel;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.Utils;
using UnoLisServer.Common.Models;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, ILoginManagerCallback
    {
        private LoginManagerClient _loginClient;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public LoginPage()
        {
            InitializeComponent();
        }

        public void LoginResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    HandleSuccessResponse(message);
                }
                else
                {
                    HandleErrorResponse(message);
                }
            });
        }

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            var credentials = GetCredentialsFromUI();
            if (!ValidateInput(credentials))
            {
                return;
            }
            ExecuteLogin(credentials);
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void SignUpLabelMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new RegisterPage());
        }

        private AuthCredentials GetCredentialsFromUI()
        {
            return new AuthCredentials
            {
                Nickname = NicknameTextBox.Text.Trim(),
                Password = PasswordField.Password
            };
        }

        private bool ValidateInput(AuthCredentials credentials)
        {
            List<string> errors = UserValidator.ValidateLogin(credentials);
            if (errors.Count > 0)
            {
                string message = "◆ " + string.Join("\n◆ ", errors);
                ShowAlert(Global.WarningLabel, message);
                return false;
            }
            return true;
        }

        private void ExecuteLogin(AuthCredentials credentials)
        {
            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _loginClient = new LoginManagerClient(context);
                _loginClient.Login(credentials);
            }
            catch (EndpointNotFoundException enfEx)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, enfEx);
            }
            catch (TimeoutException toEx)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, toEx);
            }
            catch (CommunicationException comEx)
            {
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, comEx);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
            }
        }

        private void HandleSuccessResponse(string message)
        {
            CurrentSession.CurrentUserNickname = NicknameTextBox.Text.Trim();
            ShowAlert(Global.SuccessLabel, message);
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void HandleErrorResponse(string message)
        {
            ShowAlert(Global.UnsuccessfulLabel, message);
        }

        private void HandleException(string userMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error($"Fallo en el inicio de sesión: {ex.Message}", ex);
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
