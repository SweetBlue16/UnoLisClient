using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for HowToPlayPage.xaml
    /// </summary>
    public partial class HowToPlayPage : Page, INavigationService
    {
        public HowToPlayPage()
        {
            InitializeComponent();
            DataContext = new HowToPlayViewModel(this);
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
