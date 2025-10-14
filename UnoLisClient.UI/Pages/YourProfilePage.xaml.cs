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
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, IProfileManagerCallback
    {
        private ProfileManagerClient _profileClient;

        public YourProfilePage()
        {
            InitializeComponent();
            try
            {
                _profileClient = new ProfileManagerClient(new InstanceContext(this));
                string nickname = SessionManager.CurrentProfile?.Nickname ?? Global.GuestLabel;
                _profileClient.GetProfileData(nickname);
            }
            catch
            {
                LoadDefaultData();
            }
        }

        public void ProfileDataReceived(ProfileData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UserNicknameLabel.Text = data.Nickname;
                UserFullNameLabel.Text = data.FullName;
                UserEmailLabel.Text = data.Email;

                PlayerStatisticsDataGrid.ItemsSource = new List<dynamic>
                {
                    new
                    {
                        MatchesPlayed = data.MatchesPlayed,
                        Wins = data.Wins,
                        Loses = data.Losses,
                        GlobalPoints = data.ExperiencePoints,
                        WinRate = data.MatchesPlayed == 0 ? "0%" : 
                            $"{(int)((float)data.Wins / data.MatchesPlayed * 100)}%"
                    }
                };
            });
        }

        public void ProfileUpdateResponse(bool success, string message) {}

        private void ClickChangeAvatarButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AvatarSelectionPage());
        }

        private void ClickChangeDataButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditProfilePage());
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void LoadDefaultData()
        {
            var defaultData = new ProfileData
            {
                Nickname = Global.GuestLabel,
                FullName = "-",
                Email = "-",
                MatchesPlayed = 0,
                Wins = 0,
                Losses = 0,
                ExperiencePoints = 0
            };
            ProfileDataReceived(defaultData);
        }
    }
}
