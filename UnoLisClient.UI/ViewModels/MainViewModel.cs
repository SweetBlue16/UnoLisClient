using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILogoutService _logoutService;
        private readonly INavigationService _navigationService;

        public MainViewModel( INavigationService navigationService, IDialogService dialogService, 
            ILogoutService logoutService): base(dialogService)
        {
            _navigationService = navigationService;
            _logoutService = logoutService;
        }

        public void NavigateToInitialPage()
        {
            _navigationService.NavigateTo(new GamePage());
        }

        public async Task<bool> TryLogoutAndCloseAsync()
        {
            if (string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname))
            {
                return true;
            }

            bool confirm = _dialogService.ShowQuestionDialog(Global.ConfirmationLabel, Global.LogoutMessageLabel,
                PopUpIconType.Logout);

            if (!confirm)
            {
                return false;
            }

            try
            {
                var response = await _logoutService.LogoutAsync(CurrentSession.CurrentUserNickname);

                if (!response.Success)
                {
                    Logic.Helpers.LogManager.Warn($"Logout failed for user {CurrentSession.CurrentUserNickname}: {response}");
                    System.Diagnostics.Debug.WriteLine($"Logout failed: {response}");
                }
            }
            catch (TimeoutException ex)
            {
                Logic.Helpers.LogManager.Error($"Timeout during logout for user {CurrentSession.CurrentUserNickname}", ex);
                System.Diagnostics.Debug.WriteLine($"Logout Timeout: {ex.Message}");
            }
            catch (CommunicationException ex)
            {
                Logic.Helpers.LogManager.Error($"Network error during logout for user {CurrentSession.CurrentUserNickname}", ex);
                System.Diagnostics.Debug.WriteLine($"Logout Network Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logic.Helpers.LogManager.Error($"Unexpected error during logout for user {CurrentSession.CurrentUserNickname}", ex);
                System.Diagnostics.Debug.WriteLine($"Error closing session: {ex.Message}");
            }
            finally
            {
                CurrentSession.CurrentUserNickname = null;
                CurrentSession.CurrentUserProfileData = null;

                ChatService.Instance.Cleanup();
                FriendsService.Instance.Cleanup();
            }

            return true;
        }
    }
}
