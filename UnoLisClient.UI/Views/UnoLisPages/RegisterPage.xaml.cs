using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, INavigationService
    {
        public static readonly Regex EmailRegex = new Regex(@"^(?=^.{1,100}$)[^@\s]+@[^@\s]+\.[^@\s]+$");
        public static readonly Regex NicknameRegex = new Regex("^[a-zA-Z0-9_-]{1,45}$");
        public static readonly Regex FullNameRegex = new Regex(@"^[\p{L}\s]{1,45}$");

        public RegisterPage()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(this, new AlertManager());
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

        private void StrongFullNamePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !FullNameRegex.IsMatch(e.Text);
        }

        private void StrongNicknamePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !NicknameRegex.IsMatch(e.Text);
        }

        private void StrongEmailPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = EmailRegex.IsMatch(e.Text);
        }
    }
}
