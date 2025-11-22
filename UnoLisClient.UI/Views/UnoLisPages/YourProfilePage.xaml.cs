using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.Logic.Services;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, INavigationService
    {
        public YourProfilePage()
        {
            InitializeComponent();
            this.DataContext = new ProfileViewViewModel(this, new AlertManager(), new ProfileViewService());
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

        private void YourProfilePageLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProfileViewViewModel viewModel)
            {
                if (viewModel.LoadProfileCommand.CanExecute(null))
                {
                    viewModel.LoadProfileCommand.Execute(null);
                }
            }
        }
    }
}
