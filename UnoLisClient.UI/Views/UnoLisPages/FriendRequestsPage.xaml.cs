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
    /// Interaction logic for FriendRequestsPage.xaml
    /// </summary>
    public partial class FriendRequestsPage : Page
    {
        public ObservableCollection<FriendData> FriendRequests { get; set; }

        public FriendRequestsPage()
        {
            InitializeComponent();

            FriendRequests = new ObservableCollection<FriendData>
            {
                new FriendData { Nickname = "Rdgzz.Ivan" },
                new FriendData { Nickname = "Linn" },
                new FriendData { Nickname = "naess.einarr" }
            };

            FriendRequestsList.ItemsSource = FriendRequests;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FriendsPage());
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is FriendData request)
            {
                MessageBox.Show($"{request.Nickname} is now your friend!", "UNO LIS",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                FriendRequests.Remove(request);
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is FriendData request)
                FriendRequests.Remove(request);
        }
    }
}
