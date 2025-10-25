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
using UnoLisClient.UI.UnoLisServerReference.ProfileView;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Utilities;
using System.Diagnostics;
using UnoLisClient.UI.Utils;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, IProfileViewManagerCallback
    {
        private ProfileViewManagerClient _profileViewClient;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public YourProfilePage()
        {
            InitializeComponent();
            RequestProfileData();
        }

        public void ProfileDataReceived(ServiceResponse<ProfileData> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                string message = MessageTranslator.GetMessage(response.Code);
                if (!response.Success || response.Data == null)
                {
                    new SimplePopUpWindow(Global.WarningLabel, message).ShowDialog();
                    LoadDefaultData();
                    return;
                }

                var clientProfile = ProfileDataMapper.ToClientModel(response.Data);

                UserNicknameLabel.Text = clientProfile.Nickname;
                UserFullNameLabel.Text = clientProfile.FullName;
                UserEmailLabel.Text = clientProfile.Email;

                UserFacebookLink.NavigateUri = !string.IsNullOrWhiteSpace(clientProfile.FacebookUrl) ? new Uri(clientProfile.FacebookUrl) : null;
                UserInstagramLink.NavigateUri = !string.IsNullOrWhiteSpace(clientProfile.InstagramUrl) ? new Uri(clientProfile.InstagramUrl) : null;
                UserTikTokLink.NavigateUri = !string.IsNullOrWhiteSpace(clientProfile.TikTokUrl) ? new Uri(clientProfile.TikTokUrl) : null;

                PlayerStatisticsDataGrid.ItemsSource = new List<dynamic>
                {
                    new
                    {
                        MatchesPlayed = clientProfile.MatchesPlayed,
                        Wins = clientProfile.Wins,
                        Loses = clientProfile.Losses,
                        GlobalPoints = clientProfile.ExperiencePoints,
                        WinRate = clientProfile.MatchesPlayed == 0 ? "0%" :
                        $"{(int)(float) clientProfile.Wins / clientProfile.MatchesPlayed * 100}%"
                    }
                };

                ChangeAvatarButton.IsEnabled = true;
                ChangeDataButton.IsEnabled = true;
                CurrentSession.CurrentUserProfileData = clientProfile;
            });
        }

        private void ClickChangeAvatarButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new AvatarSelectionPage());
        }

        private void ClickChangeDataButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            if (CurrentSession.CurrentUserProfileData == null)
            {
                new SimplePopUpWindow(Global.WarningLabel, ErrorMessages.ProfileNotLoadedMessageLabel).ShowDialog();
                return;
            }
            NavigationService?.Navigate(new EditProfilePage(CurrentSession.CurrentUserProfileData));
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void RequestProfileData()
        {
            ClearUiBeforeLoad();
            if (string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname) || IsGuest())
            {
                LoadDefaultData();
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
                _profileViewClient = new ProfileViewManagerClient(context);
                _profileViewClient.GetProfileData(CurrentSession.CurrentUserNickname);
            }
            catch (Exception)
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
                LoadDefaultData();
            }
        }

        private bool IsGuest()
        {
            return string.Equals(CurrentSession.CurrentUserNickname, "Guest", StringComparison.OrdinalIgnoreCase);
        }

        private void ClearUiBeforeLoad()
        {
            UserNicknameLabel.Text = "...";
            UserFullNameLabel.Text = "...";
            UserEmailLabel.Text = "...";
            PlayerStatisticsDataGrid.ItemsSource = null;
        }

        private void LoadDefaultData()
        {
            var defaultData = new ProfileData
            {
                Nickname = "Guest",
                FullName = "-",
                Email = "-",
                MatchesPlayed = 0,
                Wins = 0,
                Losses = 0,
                ExperiencePoints = 0
            };

            UserNicknameLabel.Text = defaultData.Nickname;
            UserFullNameLabel.Text = defaultData.FullName;
            UserEmailLabel.Text = defaultData.Email;

            PlayerStatisticsDataGrid.ItemsSource = new List<dynamic>
            {
                new { MatchesPlayed = 0, Wins = 0, Loses = 0, GlobalPoints = 0, WinRate = "0%" }
            };

            ChangeAvatarButton.IsEnabled = false;
            ChangeDataButton.IsEnabled = false;
        }

        private void ClickSocialLink(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink link)
            {
                string target = GetSocialLinkUrl(link.Name);

                if (!string.IsNullOrWhiteSpace(target))
                {
                    BrowserHelper.OpenUrl(target);
                }
                else
                {
                    new SimplePopUpWindow(Global.WarningLabel,
                        ErrorMessages.SocialNetworkNotConfiguredMessageLabel)
                        .ShowDialog();
                }
            }

            e.Handled = true;
        }

        private string GetSocialLinkUrl(string linkName)
        {
            if (CurrentSession.CurrentUserProfileData == null)
            {
                return null;
            }

            switch (linkName)
            {
                case "UserFacebookLink":
                    return CurrentSession.CurrentUserProfileData.FacebookUrl;
                case "UserInstagramLink":
                    return CurrentSession.CurrentUserProfileData.InstagramUrl;
                case "UserTikTokLink":
                    return CurrentSession.CurrentUserProfileData.TikTokUrl;
                default:
                    return null;
            }
        }
    }
}
