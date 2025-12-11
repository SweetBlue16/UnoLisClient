using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Mappers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.ProfileEdit;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class ProfileEditViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ProfileEditService _profileEditService;
        private readonly Page _view;
        private readonly ClientProfileData _originalProfileData;

        private string _originalEmail;
        private string _verificationCode;

        public string Nickname { get; private set; }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _facebookUrl;
        public string FacebookUrl
        {
            get => _facebookUrl;
            set => SetProperty(ref _facebookUrl, value);
        }

        private string _instagramUrl;
        public string InstagramUrl
        {
            get => _instagramUrl;
            set => SetProperty(ref _instagramUrl, value);
        }

        private string _tikTokUrl;
        public string TikTokUrl
        {
            get => _tikTokUrl;
            set => SetProperty(ref _tikTokUrl, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ProfileEditViewModel(Page view, IDialogService dialogService, ClientProfileData currentProfile)
            : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)_view;
            _profileEditService = new ProfileEditService();
            _originalProfileData = currentProfile;

            LoadProfileDataIntoViewModel();

            SaveCommand = new RelayCommand(async () => await ExecuteSaveAsync(), () => !IsLoading);
            CancelCommand = new RelayCommand(ExecuteCancel, () => !IsLoading);
        }

        private async Task ExecuteSaveAsync()
        {
            SoundManager.PlayClick();

            if (!HasChanges())
            {
                _dialogService.ShowWarning(ErrorMessages.NoChangesMessageLabel);
                return;
            }

            var updatedProfileData = GetProfileDataFromViewModel();
            var validationErrors = UserValidator.ValidateProfileUpdate(updatedProfileData);
            if (validationErrors.Any())
            {
                _dialogService.HandleValidationErrors(validationErrors.ToList());
                return;
            }

            bool emailChanged = _originalEmail != updatedProfileData.Email;
            if (emailChanged)
            {
                bool isVerified = await HandleEmailChanged();
                if (!isVerified)
                {
                    return;
                }
                updatedProfileData.VerificationCode = _verificationCode;
            }

            SetLoading(true);
            try
            {
                var response = await _profileEditService.UpdateProfileAsync(updatedProfileData);
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    CurrentSession.CurrentUserProfileData = ProfileDataMapper.ToClientModel(updatedProfileData);
                    _dialogService.ShowAlert(Global.SuccessLabel, message, PopUpIconType.Success);
                    _navigationService.NavigateTo(new YourProfilePage());
                }
                else
                {
                    _dialogService.ShowWarning(message);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al actualizar los datos del perfil: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al actualizar los datos del perfil: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al actualizar los datos del perfil: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al actualizar los datos del perfil: {ex.Message}";
                HandleException(MessageCode.ProfileUpdateFailed, logMessage, ex);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async Task<bool> HandleEmailChanged()
        {
            SetLoading(true);
            try
            {
                var response = await _profileEditService.RequestVerificationCodeAsync(Nickname, Email.Trim());
                if (!response.Success)
                {
                    string errorMessage = MessageTranslator.GetMessage(response.Code);
                    _dialogService.ShowWarning(errorMessage);
                    return false;
                }
                SetLoading(false);
                var inputDialogPopUp = new InputPopUpWindow(Global.ConfirmationLabel,
                    Global.ConfirmationMessageLabel,
                    Global.CodeLabel,
                    PopUpIconType.EmailVerification
                );
                bool? result = inputDialogPopUp.ShowDialog();

                if (result == true || !string.IsNullOrWhiteSpace(inputDialogPopUp.UserInput))
                {
                    _verificationCode = inputDialogPopUp.UserInput.Trim();
                    return true;
                }
                else
                {
                    _dialogService.ShowWarning(Global.TaskCanceledLabel);
                    return false;
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al enviar el código de verificación por correo electrónico: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
                return false;
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al enviar el código de verificación por correo electrónico: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
                return false;
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al enviar el código de verificación por correo electrónico: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
                return false;
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al enviar el código de verificación por correo electrónico: {ex.Message}";
                HandleException(MessageCode.EmailSendingFailed, logMessage, ex);
                return false;
            }
            finally
            {
                if (IsLoading)
                {
                    SetLoading(false);
                }
            }
        }

        private void ExecuteCancel()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }

        private bool HasChanges()
        {
            bool AreDifferent(string currentValue, string originalValue)
            {
                string normalizedCurrent = string.IsNullOrWhiteSpace(currentValue) ? null : currentValue.Trim();
                string normalizedOriginal = string.IsNullOrWhiteSpace(originalValue) ? null : originalValue.Trim();
                return normalizedCurrent != normalizedOriginal;
            }

            if (AreDifferent(FullName, _originalProfileData.FullName))
            {
                return true;
            }
            if (AreDifferent(Email, _originalProfileData.Email))
            {
                return true;
            }
            if (AreDifferent(FacebookUrl, _originalProfileData.FacebookUrl))
            {
                return true;
            }
            if (AreDifferent(InstagramUrl, _originalProfileData.InstagramUrl))
            {
                return true;
            }
            if (AreDifferent(TikTokUrl, _originalProfileData.TikTokUrl))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(Password))
            {
                return true;
            }

            return false;
        }

        private void LoadProfileDataIntoViewModel()
        {
            Nickname = _originalProfileData.Nickname;
            FullName = _originalProfileData.FullName;
            Email = _originalProfileData.Email;
            Password = "";
            FacebookUrl = _originalProfileData.FacebookUrl;
            InstagramUrl = _originalProfileData.InstagramUrl;
            TikTokUrl = _originalProfileData.TikTokUrl;

            _originalEmail = _originalProfileData.Email;
        }

        private ProfileData GetProfileDataFromViewModel()
        {
            return new ProfileData
            {
                Nickname = this.Nickname.Trim(),
                FullName = this.FullName.Trim(),
                Email = this.Email.Trim(),
                Password = this.Password.Trim(),
                FacebookUrl = this.FacebookUrl.Trim(),
                InstagramUrl = this.InstagramUrl.Trim(),
                TikTokUrl = this.TikTokUrl.Trim()
            };
        }

        private void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (CancelCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (isLoading)
            {
                _dialogService.ShowLoading(_view);
            }
            else
            {
                _dialogService.HideLoading();
            }
        }
    }
}
