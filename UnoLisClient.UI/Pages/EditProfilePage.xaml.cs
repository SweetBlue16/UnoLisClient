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
using System.ServiceModel;
using UnoLisClient.UI.UnoLisServerReference.Profile;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page, IProfileManagerCallback
    {
        private ProfileManagerClient _profileClient;

        public EditProfilePage()
        {
            InitializeComponent();
            _profileClient = new ProfileManagerClient(new InstanceContext(this));

            if (SessionManager.CurrentProfile != null)
            {
                _profileClient.GetProfileData(SessionManager.CurrentProfile.Nickname);
            }
        }

        public void ProfileDataReceived(ProfileData data)
        {
            SessionManager.CurrentProfile = data;
            Dispatcher.Invoke(() =>
            {
                FullNameTextBox.Text = data.FullName;
                NicknameTextBox.Text = data.Nickname;
                EmailTextBox.Text = data.Email;
                PasswordField.Password = string.Empty;
                FacebookLinkTextBox.Text = string.Empty;
                InstagramLinkTextBox.Text = string.Empty;
                TikTokLinkTextBox.Text = string.Empty;

                NicknameTextBox.IsEnabled = false;
            });
        }

        public void ProfileUpdateResponse(bool success, string message)
        {
            Dispatcher.Invoke(() =>
            {
                new SimplePopUpWindow(success ? Global.SuccessLabel : Global.UnsuccessfulLabel, message);

                if (success)
                {
                    NavigationService?.Navigate(new YourProfilePage());
                }
            });
        }

        private void ClickSaveButton(object sender, RoutedEventArgs e)
        {
            var updated = new ProfileData
            {
                Nickname = NicknameTextBox.Text.Trim(),
                FullName = FullNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim()
            };
            _profileClient.UpdateProfileData(updated);
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new YourProfilePage());
        }
    }
}
