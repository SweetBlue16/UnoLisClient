using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for AvatarSelectionPage.xaml
    /// </summary>
    public partial class AvatarSelectionPage : Page, INavigationService
    {
        public AvatarSelectionPage()
        {
            InitializeComponent();
            DataContext = new AvatarSelectionViewModel(this, new AlertManager());
            Loaded += AvatarSelectionPageLoaded;
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

        private void AvatarSelectionPageLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AvatarSelectionViewModel viewModel)
            {
                viewModel.LoadAvatarsCommand.Execute(null);
            }
        }
    }
}
