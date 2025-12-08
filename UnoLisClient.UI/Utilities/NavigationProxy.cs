using System;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Models; 
using UnoLisClient.UI.Properties;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Properties.Langs; 
using UnoLisClient.UI.Views.PopUpWindows; 
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.Logic.Helpers;

namespace UnoLisClient.UI.Utilities
{
    /// <summary>
    /// Allows navigation control from non-UI classes.
    /// </summary>
    public static class NavigationProxy
    {
        private static Frame _mainFrame;
        private static bool _isExiting = false;

        public static void Initialize(Frame frame)
        {
            _mainFrame = frame;
        }

        public static void ForceLogoutToLogin(string messageKey)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_isExiting)
                {
                    return;
                }

                _isExiting = true;

                CurrentSession.CurrentUserNickname = null;
                CurrentSession.CurrentUserProfileData = null;

                ResetAllServices();

                if (_mainFrame != null)
                {
                    _mainFrame.Navigate(new GamePage());

                    while (_mainFrame.CanGoBack)
                    {
                        _mainFrame.RemoveBackEntry();
                    }
                }

                string message = UnoLisClient.UI.Properties.Langs.Global.ResourceManager.GetString(messageKey)
                 ?? "Connection lost. Please log in again.";

                new SimplePopUpWindow(Global.OopsLabel, message, PopUpIconType.Error).ShowDialog();

                _isExiting = false;
            });
        }

        private static void ResetAllServices()
        {
            try
            {
                ChatService.Instance.Cleanup();
                FriendsService.Instance.Cleanup();
                GameplayService.Instance.Cleanup();

            }
            catch (InvalidOperationException invEx)
            {
                Logger.Warn("Attempted to clean up services during forced logout, " +
                    "but an operation was invalid: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error cleaning up services during forced logout.", ex);
            }
        }
    }
}