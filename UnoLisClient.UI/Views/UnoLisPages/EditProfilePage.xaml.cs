using System.Windows.Controls;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.Logic.Models;
using System.Windows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page, INavigationService
    {
        public EditProfilePage(ClientProfileData currentProfile)
        {
            InitializeComponent();
            DataContext = new ProfileEditViewModel(this, new AlertManager(), currentProfile);
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
