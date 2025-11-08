using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.Models; 
namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class FriendsPage : Page
    {
        public FriendsPage()
        {
            InitializeComponent();

            IFriendsService friendsService = new FriendsService();
            IModalService modalService = new DialogService();

            DataContext = new FriendsViewModel(friendsService, modalService);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void OpenFriendRequests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new FriendRequestsPage());
        }
    }
}