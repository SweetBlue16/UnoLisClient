using System;
using System.Windows;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Utilities
{
    public static class BrowserHelper
    {
        public static void CopyUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            try
            {
                Clipboard.SetText(url);

                Application.Current?.Dispatcher.Invoke(() =>
                {
                    new SimplePopUpWindow(
                        Global.SuccessLabel,
                        Global.SocialMediaCopiedLabel,
                        PopUpIconType.Success
                    ).ShowDialog();
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"No se pudo copiar la URL al portapapeles: {url}", ex);

                Application.Current?.Dispatcher.Invoke(() =>
                {
                    new SimplePopUpWindow(
                        Global.WarningLabel,
                        ErrorMessages.SocialNetworkNotConfiguredMessageLabel,
                        PopUpIconType.Warning
                    ).ShowDialog();
                });
            }
        }
    }
}
