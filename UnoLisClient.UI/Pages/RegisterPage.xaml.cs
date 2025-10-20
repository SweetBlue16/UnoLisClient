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
using UnoLisClient.UI.UnoLisServerReference.Login;
using UnoLisClient.UI.UnoLisServerReference.Register;
using UnoLisClient.UI.Validators;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IRegisterManagerCallback
    {
        private RegisterManagerClient _registerClient;

        public RegisterPage()
        {
            InitializeComponent();
        }

        public void RegisterResponse(bool success, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (success)
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

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void ClickSignInButton(object sender, RoutedEventArgs e)
        {
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
                string message = string.Join("\n◆ ", errors);
                new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                return;
            }

            try
            {
                var context = new InstanceContext(this);
                _registerClient = new RegisterManagerClient(context);
                _registerClient.Register(registrationData);
            }
            catch (Exception)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
            }
        }
    }
}
