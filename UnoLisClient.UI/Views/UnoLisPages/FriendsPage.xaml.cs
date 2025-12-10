using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.Models;
using UnoLisClient.UI.Utilities;
namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class FriendsPage : Page
    {
        public FriendsPage()
        {
            InitializeComponent();

            DataContext = new FriendsViewModel(FriendsService.Instance, new AlertManager());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void OpenFriendRequestsClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new FriendRequestsPage());
        }
    }
}