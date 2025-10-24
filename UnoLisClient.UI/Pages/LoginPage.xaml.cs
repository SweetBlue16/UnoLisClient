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
                _loadingPopUpWindow?.StopLoadingAndClose();
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    CurrentSession.CurrentUserNickname = NicknameTextBox.Text.Trim();
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                    NavigationService?.Navigate(new MainMenuPage());
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            string nickname = NicknameTextBox.Text.Trim();
            string password = PasswordField.Password;
            var credentials = new AuthCredentials
            {
                Nickname = nickname,
                Password = password
            };

            List<string> errors = UserValidator.ValidateLogin(credentials);
            if (errors.Count > 0)
            {
                string message = "◆ " + string.Join("\n◆ ", errors);
                new SimplePopUpWindow(Global.WarningLabel, message).ShowDialog();
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
                _loginClient = new LoginManagerClient(context);
                _loginClient.Login(credentials);
            }
            catch (Exception ex)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
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
    }
}
