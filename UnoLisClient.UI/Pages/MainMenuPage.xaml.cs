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
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.UnoLisServerReference.Logout;
using UnoLisClient.UI.Utils;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page, ILogoutManagerCallback
    {
        private LogoutManagerClient _logoutClient;

        public MainMenuPage()
        {
            InitializeComponent();
        }

        public void LogoutResponse(bool success, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (success)
                {
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new GameSettingsPage());
        }

        private void SettingsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new SettingsPage());
        }

        private void ShopLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new AvatarShopPage());
        }

        private void ProfileLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void ExitLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            var result = new QuestionPopUpWindow(Global.ConfirmationLabel, Global.LogoutMessageLabel).ShowDialog();
            if (result == true)
            {
                NavigationService?.Navigate(new GamePage());
                LogoutCurrentUser();
            }
        }

        private void LogoutCurrentUser()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname))
                {
                    var context = new InstanceContext(this);
                    _logoutClient = new LogoutManagerClient(context);
                    _logoutClient.LogoutAsync(CurrentSession.CurrentUserNickname);

                    CurrentSession.CurrentUserNickname = null;
                    CurrentSession.CurrentUserProfileData = null;
                }
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }
    }
}
