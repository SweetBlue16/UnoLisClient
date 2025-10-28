using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.UnoLisServerReference.Logout;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Utils;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page, ILogoutManagerCallback
    {
        private LogoutManagerClient _logoutClient;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public MainMenuPage()
        {
            InitializeComponent();
        }

        public void LogoutResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    ShowAlert(Global.SuccessLabel, message);
                }
                else
                {
                    ShowAlert(Global.UnsuccessfulLabel, message);
                }
                ClearLocalSessionAndNavigate();
            });
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new MatchMenuPage());
        }

        private void SettingsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new SettingsPage());
        }

        private void ShopLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new AvatarShopPage());
        }

        private void ProfileLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void ExitLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SoundManager.PlayClick();
            var result = new QuestionPopUpWindow(Global.ConfirmationLabel, Global.LogoutMessageLabel).ShowDialog();
            if (result == true)
            {
                NavigationService?.Navigate(new GamePage());
                ExecuteUserLogout();
            }
        }

        private bool IsGuest()
        {
            return string.Equals(CurrentSession.CurrentUserNickname, "Guest", StringComparison.OrdinalIgnoreCase);
        }

        private void ExecuteUserLogout()
        {
            if (IsGuest())
            {
                HandleGuestLogout();
                return;
            }

            string nickname = CurrentSession.CurrentUserNickname;
            if (string.IsNullOrWhiteSpace(nickname))
            {
                ClearLocalSessionAndNavigate();
                return;
            }

            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _logoutClient = new LogoutManagerClient(context);
                _logoutClient.LogoutAsync(nickname);
            }
            catch (EndpointNotFoundException ex)
            {
                HandleException(ErrorMessages.ConnectionRejectedMessageLabel, ex);
            }
            catch (TimeoutException ex)
            {
                HandleException(ErrorMessages.TimeoutMessageLabel, ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(ErrorMessages.ConnectionErrorMessageLabel, ex);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UnknownErrorMessageLabel, ex);
            }
        }

        private void HandleGuestLogout()
        {
            ClearLocalSessionAndNavigate();
        }

        private void HandleException(string userMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error($"Fallo en logout de MainMenu: {ex.Message}", ex);
            ShowAlert(Global.UnsuccessfulLabel, userMessage);
            ClearLocalSessionAndNavigate();
        }

        private void ClearLocalSessionAndNavigate()
        {
            CurrentSession.CurrentUserNickname = null;
            CurrentSession.CurrentUserProfileData = null;
            NavigationService?.Navigate(new GamePage());
        }

        private void ShowAlert(string title, string message)
        {
            new SimplePopUpWindow(title, message).ShowDialog();
        }

        private void ShowLoading()
        {
            _loadingPopUpWindow = new LoadingPopUpWindow()
            {
                Owner = Window.GetWindow(this)
            };
            _loadingPopUpWindow.Show();
        }

        private void HideLoading()
        {
            _loadingPopUpWindow?.StopLoadingAndClose();
        }
    }
}
