using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Utilities
{
    public static class AlertManager
    {
        private static LoadingPopUpWindow _loadingPopUpWindow;

        public static void ShowAlert(string title, string message)
        {
            new SimplePopUpWindow(title, message).ShowDialog();
        }

        public static void HandleWarning(string message)
        {
            ShowAlert(Global.UnsuccessfulLabel, message);
        }

        public static void ShowLoading(Page page)
        {
            _loadingPopUpWindow = new LoadingPopUpWindow()
            {
                Owner = Window.GetWindow(page)
            };
            _loadingPopUpWindow.Show();
        }

        public static void HideLoading()
        {
            _loadingPopUpWindow?.StopLoadingAndClose();
        }

        public static void HandleException(string userMessage, string logMessage, Exception ex)
        {
            HideLoading();
            LogManager.Error(logMessage, ex);
            ShowAlert(Global.UnsuccessfulLabel, userMessage);
        }

        public static void HandleValidationErrors(List<string> validationErrors)
        {
            string message = "◆ " + string.Join("\n◆ ", validationErrors);
            ShowAlert(Global.WarningLabel, message);
        }
    }
}
