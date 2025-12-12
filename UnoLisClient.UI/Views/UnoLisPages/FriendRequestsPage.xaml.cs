using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class FriendRequestsPage : Page
    {
        public FriendRequestsPage()
        {
            InitializeComponent();

            DataContext = new FriendRequestsViewModel(FriendsService.Instance, new AlertManager());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FriendsPage());
        }
    }
}