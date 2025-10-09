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

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            string nickname = NicknameTextBox.Text;
            string password = PasswordBox.Password;
            NavigationService?.Navigate(new MainMenuPage());
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
