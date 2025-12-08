using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Utilities
{
    public static class BrowserHelper
    {
        private const string HttpPrefix = "http://";
        private const string HttpsPrefix = "https://";

        private static readonly string[] BlockedDomains =
        {
            "bit.ly", "tinyurl.com", "shorturl.at", "t.co"
        };

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

                if (!IsSafeUrl(url))
                {
                    Logger.Warn($"Intento de abrir URL insegura o mal formada: {url}");
                    HandleBrowserFallback(url);
                    return;
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
            catch (Win32Exception winEx)
            {
                Logger.Error($"Error de Win32 al abrir URL '{url}'.", winEx);
                HandleBrowserFallback(url);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error inesperado al abrir URL '{url}'.", ex);
                HandleBrowserFallback(url);
            }
        }

        private static bool IsSafeUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                return false;
            }

            if (url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (url.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (Regex.IsMatch(url, @"[\u0000-\u001F]"))
            {
                return false;
            }

            foreach (var domain in BlockedDomains)
            {
                if (uri.Host.IndexOf(domain, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static void HandleBrowserFallback(string url)
        {
            try
            {
                Clipboard.SetText(url);
            }
            catch (Exception clipboardEx)
            {
                Logger.Error("No se pudo copiar la URL al portapapeles.", clipboardEx);
            }

            ShowBrowserError();
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
