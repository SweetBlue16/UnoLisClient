using System;
using System.ComponentModel;
using System.Diagnostics;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.Logic.Helpers;
using System.Windows;

// TODO: Debug
namespace UnoLisClient.UI.Utilities
{
    public static class BrowserHelper
    {
        private const string HttpPrefix = "http://";
        private const string HttpsPrefix = "https://";

        public static void OpenUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            try
            {
                if (!url.StartsWith(HttpPrefix, StringComparison.OrdinalIgnoreCase) &&
                    !url.StartsWith(HttpsPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    url = HttpsPrefix + url;
                }

                if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                {
                    Logger.Warn($"Intento de abrir URL mal formada: {url}");
                    ShowBrowserError();
                    return;
                }

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
            }
            catch (Win32Exception winEx)
            {
                Logger.Error($"Error de Win32 al abrir URL '{url}'.", winEx);
                ShowBrowserError();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error inesperado al abrir URL '{url}'.", ex);
                ShowBrowserError();
            }
        }

        private static void ShowBrowserError()
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                new SimplePopUpWindow(
                    Global.WarningLabel,
                    ErrorMessages.UnableToOpenLinkMessageLabel
                ).ShowDialog();
            });
        }
    }
}
