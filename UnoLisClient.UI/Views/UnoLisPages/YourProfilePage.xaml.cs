using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.Logic.Mappers;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, IProfileViewManagerCallback
    {
        private ProfileViewManagerClient _profileViewClient;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public YourProfilePage()
        {
            InitializeComponent();
            LoadProfileData();
        }

        public void ProfileDataReceived(ServiceResponse<ProfileData> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);

                if (!response.Success || response.Data == null)
                {
                    HandleResponseError(message);
                    return;
                }

                var clientProfile = ProfileDataMapper.ToClientModel(response.Data);
                PopulateUiWithProfile(clientProfile);
                PlayerStatisticsDataGrid.ItemsSource = CreateStatisticsList(clientProfile);
                EnableProfileButtons(true);
                CurrentSession.CurrentUserProfileData = clientProfile;
            });
        }

        private void ClickChangeAvatarButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new AvatarSelectionPage());
        }

        private void ClickChangeDataButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if (CurrentSession.CurrentUserProfileData == null)
            {
                ShowAlert(Global.WarningLabel, ErrorMessages.ProfileNotLoadedMessageLabel);
                return;
            }
            NavigationService?.Navigate(new EditProfilePage(CurrentSession.CurrentUserProfileData));
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void ClickSocialLink(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink link)
            {
                HandleSocialLinkClick(link.Name);
            }
            e.Handled = true;
        }

        private void LoadProfileData()
        {
            ClearUiBeforeLoad();

            if (string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname) || IsGuest())
            {
                LoadDefaultData();
                return;
            }

            ExecuteGetProfileDataCall(CurrentSession.CurrentUserNickname);
        }

        private void ExecuteGetProfileDataCall(string nickname)
        {
            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _profileViewClient = new ProfileViewManagerClient(context);
                _profileViewClient.GetProfileData(nickname);
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

        private void HandleResponseError(string message)
        {
            ShowAlert(Global.WarningLabel, message);
            LoadDefaultData();
        }

        private void HandleException(string userMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error($"Fallo al cargar perfil: {ex.Message}", ex);
            ShowAlert(Global.UnsuccessfulLabel, userMessage);
            LoadDefaultData();
        }

        private void HandleSocialLinkClick(string linkName)
        {
            if (CurrentSession.CurrentUserProfileData == null)
            {
                ShowAlert(Global.WarningLabel, ErrorMessages.ProfileNotLoadedMessageLabel);
                return;
            }

            string targetUrl = null;
            switch (linkName)
            {
                case "UserFacebookLink":
                    targetUrl = CurrentSession.CurrentUserProfileData.FacebookUrl;
                    break;
                case "UserInstagramLink":
                    targetUrl = CurrentSession.CurrentUserProfileData.InstagramUrl;
                    break;
                case "UserTikTokLink":
                    targetUrl = CurrentSession.CurrentUserProfileData.TikTokUrl;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(targetUrl))
            {
                BrowserHelper.OpenUrl(targetUrl);
            }
            else
            {
                ShowAlert(Global.WarningLabel, ErrorMessages.SocialNetworkNotConfiguredMessageLabel);
            }
        }

        private bool IsGuest()
        {
            return string.Equals(CurrentSession.CurrentUserNickname, "Guest", StringComparison.OrdinalIgnoreCase);
        }

        private void PopulateUiWithProfile(ClientProfileData clientProfile)
        {
            UserNicknameLabel.Text = clientProfile.Nickname;
            UserFullNameLabel.Text = clientProfile.FullName;
            UserEmailLabel.Text = clientProfile.Email;

            UserFacebookLink.NavigateUri = CreateUri(clientProfile.FacebookUrl);
            UserInstagramLink.NavigateUri = CreateUri(clientProfile.InstagramUrl);
            UserTikTokLink.NavigateUri = CreateUri(clientProfile.TikTokUrl);
        }

        private List<dynamic> CreateStatisticsList(ClientProfileData clientProfile)
        {
            string winRate = "0%";
            if (clientProfile.MatchesPlayed > 0)
            {
                double rate = (double)clientProfile.Wins / clientProfile.MatchesPlayed * 100;
                winRate = $"{(int)rate}%";
            }

            return new List<dynamic>
            {
                new
                {
                    MatchesPlayed = clientProfile.MatchesPlayed,
                    Wins = clientProfile.Wins,
                    Loses = clientProfile.Losses,
                    GlobalPoints = clientProfile.ExperiencePoints,
                    WinRate = winRate
                }
            };
        }

        private void LoadDefaultData()
        {
            ClearUiBeforeLoad();
            UserNicknameLabel.Text = "Guest";
            UserFullNameLabel.Text = "-";
            UserEmailLabel.Text = "-";

            PlayerStatisticsDataGrid.ItemsSource = new List<dynamic>
            {
                new { MatchesPlayed = 0, Wins = 0, Loses = 0, GlobalPoints = 0, WinRate = "0%" }
            };

            EnableProfileButtons(false);
        }

        private void ClearUiBeforeLoad()
        {
            UserNicknameLabel.Text = "...";
            UserFullNameLabel.Text = "...";
            UserEmailLabel.Text = "...";
            PlayerStatisticsDataGrid.ItemsSource = null;
            EnableProfileButtons(false);
        }

        private void EnableProfileButtons(bool isEnabled)
        {
            ChangeAvatarButton.IsEnabled = isEnabled;
            ChangeDataButton.IsEnabled = isEnabled;
        }

        private Uri CreateUri(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return uri;
            }
            return null;
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
