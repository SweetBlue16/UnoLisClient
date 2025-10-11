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

namespace UnoLisClient.UI.Pages
{
    // Class for test purposes
    public class PlayerStats
    {
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int GlobalPoints { get; set; }
        public string WinRate { get; set; }
    }

    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page
    {
        public YourProfilePage()
        {
            InitializeComponent();
            // Test data for demonstration purposes
            var stats = new List<PlayerStats>
            {
                new PlayerStats { MatchesPlayed = 150, Wins = 90, Loses = 60, GlobalPoints = 1200, WinRate = "60%" }
            };
            PlayerStatisticsDataGrid.ItemsSource = stats;
        }

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
            NavigationService?.GoBack();
        }
    }
}
