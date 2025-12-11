using System.Windows.Controls;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using System.Windows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for MatchMenuPage.xaml
    /// </summary>
    public partial class MatchMenuPage : Page, INavigationService
    {
        public MatchMenuPage()
        {
            InitializeComponent();
            DataContext = new MatchMenuViewModel(this, new AlertManager());
        }

        public void NavigateTo(Page page)
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.Navigate(page)
            );
        }

        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.GoBack()
            );
        }
    }
}

