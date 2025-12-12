using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, INavigationService
    {
        public static readonly Regex _nicknameRegex = new Regex("^[a-zA-Z0-9_-]{1,45}$");

        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(new AlertManager(), this);
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

        private void StrongNicknamePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !_nicknameRegex.IsMatch(e.Text);
        }
    }
}
