using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Models;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page, INavigationService
    {
        public static readonly Regex EmailRegex = new Regex(@"^(?=^.{1,100}$)[^@\s]+@[^@\s]+\.[^@\s]+$");
        public static readonly Regex NicknameRegex = new Regex("^[a-zA-Z0-9_-]{1,45}$");
        public static readonly Regex FullNameRegex = new Regex(@"^[\p{L}\s]{1,45}$");
        public static readonly Regex FacebookUrlRegex = new Regex(
        @"^(?=^.{1,255}$)(https?:\/\/)?(www\.)?facebook\.com\/[a-zA-Z0-9(\.\?)?]+$");
        public static readonly Regex InstagramUrlRegex = new Regex(
        @"^(?=^.{1,255}$)(https?:\/\/)?(www\.)?instagram\.com\/[a-zA-Z0-9_.]+$");
        public static readonly Regex TikTokUrlRegex = new Regex(
        @"^(?=^.{1,255}$)(https?:\/\/)?(www\.)?tiktok\.com\/@?[a-zA-Z0-9_.]+$");

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

        private void StrongFacebookLinkPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = FacebookUrlRegex.IsMatch(e.Text);
        }

        private void StrongInstagramLinkPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = InstagramUrlRegex.IsMatch(e.Text);
        }

        private void StrongTikTokLinkPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = TikTokUrlRegex.IsMatch(e.Text);
        }
    }
}
