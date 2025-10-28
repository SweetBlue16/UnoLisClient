using System;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using UnoLisClient.UI.UnoLisServerReference.ProfileEdit;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Validators;
using UnoLisClient.UI.Utils;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page, IProfileEditManagerCallback
    {
        private ProfileEditManagerClient _profileEditClient;
        private readonly ClientProfileData _currentProfile;
        private LoadingPopUpWindow _loadingPopUpWindow;

        public EditProfilePage(ClientProfileData currentProfile)
        {
            InitializeComponent();
            _currentProfile = currentProfile ?? new ClientProfileData();
            LoadProfileDataIntoUI();
        }

        public void ProfileUpdateResponse(ServiceResponse<ProfileData> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HideLoading();
                string message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    HandleSuccessResponse(message);
                }
                else
                {
                    HandleErrorResponse(message);
                }
            });
        }

        private void ClickSaveButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            var updatedProfile = GetProfileDataFromUI();
            var contractProfile = updatedProfile.ToProfileEditContract();

            if (!ValidateInput(contractProfile))
            {
                return;
            }
            ExecuteUpdateProfile(contractProfile);
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void ExecuteUpdateProfile(ProfileData contractProfile)
        {
            try
            {
                ShowLoading();
                var context = new InstanceContext(this);
                _profileEditClient = new ProfileEditManagerClient(context);
                _profileEditClient.UpdateProfileData(contractProfile);
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

        private void HandleSuccessResponse(string message)
        {
            ShowAlert(Global.SuccessLabel, message);
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void HandleErrorResponse(string message)
        {
            ShowAlert(Global.UnsuccessfulLabel, message);
        }

        private void HandleException(string userMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error($"Fallo al actualizar perfil: {ex.Message}", ex);
            ShowAlert(Global.UnsuccessfulLabel, userMessage);
        }

        private void LoadProfileDataIntoUI()
        {
            NicknameTextBox.Text = _currentProfile.Nickname;
            FullNameTextBox.Text = _currentProfile.FullName;
            EmailTextBox.Text = _currentProfile.Email;
            FacebookLinkTextBox.Text = _currentProfile.FacebookUrl;
            InstagramLinkTextBox.Text = _currentProfile.InstagramUrl;
            TikTokLinkTextBox.Text = _currentProfile.TikTokUrl;
        }

        private ClientProfileData GetProfileDataFromUI()
        {
            return new ClientProfileData
            {
                Nickname = _currentProfile.Nickname,
                FullName = FullNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                Password = PasswordField.Password,
                FacebookUrl = FacebookLinkTextBox.Text.Trim(),
                InstagramUrl = InstagramLinkTextBox.Text.Trim(),
                TikTokUrl = TikTokLinkTextBox.Text.Trim()
            };
        }

        private bool ValidateInput(ProfileData contractProfile)
        {
            var errors = UserValidator.ValidateProfileUpdate(contractProfile);
            if (errors.Count > 0)
            {
                string message = "◆ " + string.Join("\n◆ ", errors);
                ShowAlert(Global.WarningLabel, message);
                return false;
            }
            return true;
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
