using System.Windows;
using System.Windows.Controls;
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
            DataContext = new LeaderboardsViewModel(this, new AlertManager());
            Loaded += (s, e) =>
            {
                if (DataContext is LeaderboardsViewModel viewModel)
                {
                    viewModel.LoadLeaderboardsCommand.Execute(null);
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
