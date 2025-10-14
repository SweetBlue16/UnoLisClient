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
using UnoLisClient.UI.UnoLisServerReference;
using UnoLisClient.UI.UnoLisServerReference.Auth;
using UnoLisClient.UI.UnoLisServerReference.Profile;
using UnoLisClient.UI.Validators;
using System.ServiceModel;
using UnoLisClient.UI.Managers;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IAuthManagerCallback
    {
        private AuthManagerClient _authClient;

        public LoginPage()
        {
            InitializeComponent();
        }

        public void LoginResponse(bool success, string message)
        {
            if (success)
            {
                SessionManager.CurrentProfile = new ProfileData
                {
                    Nickname = NicknameTextBox.Text.Trim()
                };
                new SimplePopUpWindow(Global.SuccessLabel, SignIn.WelcomeLabel).ShowDialog();
                NavigationService?.Navigate(new MainMenuPage());
            }
            else
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
            }
        }

        public void RegisterResponse(bool success, string message) {}

        public void ConfirmationResponse(bool success) {}

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            string nickname = NicknameTextBox.Text.Trim();
            string password = PasswordField.Password.Trim();

            var errors = UserValidator.ValidateLoginEmptyFields(nickname, password);
            if (errors.Any())
            {
                string errorMessage = string.Join("\n", errors);
                new SimplePopUpWindow(errorMessage, Global.WarningLabel).ShowDialog();
                return;
            }

            _authClient = new AuthManagerClient(new InstanceContext(this));
            var credentials = new AuthCredentials
            {
                Nickname = nickname,
                Password = password
            };
            _authClient.Login(credentials);
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SignUpLabelMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new RegisterPage());
        }
    }
}
