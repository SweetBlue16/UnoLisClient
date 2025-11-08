using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class FriendRequestsPage : Page
    {
        public FriendRequestsPage()
        {
            InitializeComponent();
            IFriendsService friendsService = new FriendsService();
            IModalService modalService = new DialogService();

            DataContext = new FriendRequestsViewModel(friendsService, modalService);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FriendsPage());
        }
    }
}