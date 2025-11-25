using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for LeaderboardsPage.xaml
    /// </summary>
    public partial class LeaderboardsPage : Page, INavigationService
    {
        public LeaderboardsPage()
        {
            InitializeComponent();
            DataContext = new LeaderboardsViewModel(new LeaderboardsService(), this, new AlertManager());
            Loaded += async (s, e) =>
            {
                if (DataContext is LeaderboardsViewModel viewModel)
                {
                    await viewModel.LoadLeaderboardData();
                }
            };
        }

        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.GoBack()
            );
        }

        public void NavigateTo(Page page)
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.Navigate(page)
            );
        }
    }
}
