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
using UnoLisClient.UI.UnoLisServerReference.ProfileEdit;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Validators;
using UnoLisClient.UI.Utils;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page, IProfileEditManagerCallback
    {
        private ProfileEditManagerClient _profileEditClient;
        private readonly ClientProfileData _currentProfile;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public EditProfilePage(ClientProfileData currentProfile)
        {
            InitializeComponent();
            _currentProfile = currentProfile ?? new ClientProfileData();
            LoadProfileData();
        }

        public void ProfileUpdateResponse(bool success, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                if (success)
                {
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                    NavigationService?.Navigate(new YourProfilePage());
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        private void ClickSaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundManager.PlayClick();
                var updatedProfile = new ClientProfileData
                {
                    Nickname = _currentProfile.Nickname,
                    FullName = FullNameTextBox.Text.Trim(),
                    Email = EmailTextBox.Text.Trim(),
                    Password = PasswordField.Password,
                    FacebookUrl = FacebookLinkTextBox.Text.Trim(),
                    InstagramUrl = InstagramLinkTextBox.Text.Trim(),
                    TikTokUrl = TikTokLinkTextBox.Text.Trim()
                };

                var errors = UserValidator.ValidateProfileUpdate(updatedProfile.ToProfileEditContract(),
                    updatedProfile.Password);

                if (errors.Count > 0)
                {
                    string message = "◆ " + string.Join("\n◆ ", errors);
                    new SimplePopUpWindow(Global.WarningLabel, message).ShowDialog();
                    return;
                }

                _loadingPopUpWindow = new LoadingPopUpWindow()
                {
                    Owner = Window.GetWindow(this)
                }; 
                _loadingPopUpWindow.Show();
                var context = new InstanceContext(this);
                _profileEditClient = new ProfileEditManagerClient(context);
                var contractProfile = updatedProfile.ToProfileEditContract();
                _profileEditClient.UpdateProfileData(contractProfile);
            }
            catch (Exception)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
            }
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void LoadProfileData()
        {
            NicknameTextBox.Text = _currentProfile.Nickname;
            FullNameTextBox.Text = _currentProfile.FullName;
            EmailTextBox.Text = _currentProfile.Email;
            FacebookLinkTextBox.Text = _currentProfile.FacebookUrl;
            InstagramLinkTextBox.Text = _currentProfile.InstagramUrl;
            TikTokLinkTextBox.Text = _currentProfile.TikTokUrl;
        }
    }
}
