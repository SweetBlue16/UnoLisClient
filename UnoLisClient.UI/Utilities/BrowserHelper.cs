using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;

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

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                new SimplePopUpWindow(Global.WarningLabel,
                    ErrorMessages.UnableToOpenLinkMessageLabel)
                    .ShowDialog();
            }
        }
    }
}
