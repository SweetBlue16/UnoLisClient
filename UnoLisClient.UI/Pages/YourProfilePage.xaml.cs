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

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, IProfileViewManagerCallback
    {
        private ProfileViewManagerClient _profileViewClient;

        public YourProfilePage()
        {
            InitializeComponent();
            RequestProfileData();
        }

        public void ProfileDataReceived(bool success, ProfileData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!success || data == null)
                {
                    new SimplePopUpWindow(Global.WarningLabel, "").ShowDialog();
                    LoadDefaultData();
                    return;
                }

                var clientProfile = data.ToClientModel();

                UserNicknameLabel.Text = data.Nickname;
                UserFullNameLabel.Text = data.FullName;
                UserEmailLabel.Text = data.Email;

                UserFacebookLinkLabel.Content = data.FacebookUrl;
                UserInstagramLinkLabel.Content = data.InstagramUrl;
                UserTikTokLinkLabel.Content = data.TikTokUrl;

                PlayerStatisticsDataGrid.ItemsSource = new List<dynamic>
                {
                    new
                    {
                        MatchesPlayed = data.MatchesPlayed,
                        Wins = data.Wins,
                        Loses = data.Losses,
                        GlobalPoints = data.ExperiencePoints,
                        WinRate = data.MatchesPlayed == 0 ? "0%" :
                        $"{(int)(float) data.Wins / data.MatchesPlayed * 100}%"
                    }
                };
                CurrentSession.CurrentUserProfileData = clientProfile;
            });
        }

        private void ClickChangeAvatarButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AvatarSelectionPage());
        }

        private void ClickChangeDataButton(object sender, RoutedEventArgs e)
        {
            if (CurrentSession.CurrentUserProfileData == null)
            {
                new SimplePopUpWindow(Global.WarningLabel, ErrorMessages.ProfileNotLoadedMessageLabel).ShowDialog();
                return;
            }
            NavigationService?.Navigate(new EditProfilePage(CurrentSession.CurrentUserProfileData));
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void RequestProfileData()
        {
            try
            {
                var context = new InstanceContext(this);
                _profileViewClient = new ProfileViewManagerClient(context);
                string nickname = CurrentSession.CurrentUserNickname ?? "Guest";
                _profileViewClient.GetProfileData(nickname);
            }
            catch (Exception)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ErrorMessages.ConnectionErrorMessageLabel).ShowDialog();
                LoadDefaultData();
            }
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
        }
    }
}
