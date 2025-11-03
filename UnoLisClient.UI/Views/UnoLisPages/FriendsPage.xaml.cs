using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        public ObservableCollection<FriendData> Friends { get; set; }

        public FriendsPage()
        {
            InitializeComponent();

            Friends = new ObservableCollection<FriendData>
            {
                new FriendData { Nickname = "SweetBlue16" },
                new FriendData { Nickname = "MapleVR" },
                new FriendData { Nickname = "Erickmel" },
                new FriendData { Nickname = "IngeAbraham" },
            };

            FriendsTable.ItemsSource = Friends;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void AddFriend_Click(object sender, RoutedEventArgs e)
        {
            AddFriendOverlay.Visibility = Visibility.Visible;
        }

        private void AddFriendModal_FriendAdded(string nickname)
        {
            AddFriendOverlay.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrWhiteSpace(nickname))
            {
                Friends.Add(new FriendData { Nickname = nickname });
                MessageBox.Show($"Friend '{nickname}' added successfully!", "UNO LIS",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddFriendModal_Canceled()
        {
            AddFriendOverlay.Visibility = Visibility.Collapsed;
        }

        private void RemoveFriend_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is FriendData friend)
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to remove {friend.Nickname}?",
                    "UNO LIS",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                    Friends.Remove(friend);
            }
        }

        private void OpenFriendRequests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new FriendRequestsPage());
        }
    }

    public class FriendData
    {
        public string Nickname { get; set; }
    }
}
