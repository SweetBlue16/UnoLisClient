using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;

namespace UnoLisClient.UI.Utilities
{
    public static class PopUpIconHelper
    {
        private const string BaseAssetPath = "pack://application:,,,/Assets/Other/";

        private const string IconSuccess = "OkAlert.png";
        private const string IconError = "ErrorAlert.png";
        private const string IconWarning = "WarningAlert.png";
        private const string IconInfo = "Info.png";
        private const string IconQuestion = "QuestionAlert.png";
        private const string IconLogout = "Logout.png";
        private const string IconLogin = "Login.png";
        private const string IconEmailVerification = "EmailVerification.png";
        private const string IconCreateAccount = "CreateAccount.png";
        private const string IconRecoverPassword = "RecoverPassword.png";
        private const string IconReportPlayer = "ReportPlayer.png";

        public static ImageSource GetIconSource(PopUpIconType iconType)
        {
            if (iconType == PopUpIconType.None)
            {
                return null;
            }

            try
            {
                string fileName = GetImagePath(iconType);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Logger.Warn($"[PopUpIconHelper.GetIconSource] No image path found for icon type: {iconType}");
                    return null;
                }
                string fullPath = $"{BaseAssetPath}{fileName}";
                var uri = new Uri(fullPath, UriKind.Absolute);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = uri;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                return bitmap;
            }
            catch (UriFormatException uriEx)
            {
                Logger.Error($"[PopUpIconHelper] Invalid URI format for icon {iconType}", uriEx);
                return null;
            }
            catch (IOException ioEx)
            {
                Logger.Error($"[PopUpIconHelper] Icon resource not found or inaccessible: {iconType}", ioEx);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error($"[PopUpIconHelper] Unexpected error loading icon {iconType}", ex);
                return null;
            }
        }

        private static string GetImagePath(PopUpIconType iconType)
        {
            switch (iconType)
            {
                case PopUpIconType.Success: return IconSuccess;
                case PopUpIconType.Error: return IconError;
                case PopUpIconType.Warning: return IconWarning;
                case PopUpIconType.Info: return IconInfo;
                case PopUpIconType.Question: return IconQuestion;
                case PopUpIconType.Logout: return IconLogout;
                case PopUpIconType.Login: return IconLogin;
                case PopUpIconType.EmailVerification: return IconEmailVerification;
                case PopUpIconType.CreateAccount: return IconCreateAccount;
                case PopUpIconType.RecoverPassword: return IconRecoverPassword;
                case PopUpIconType.ReportPlayer: return IconReportPlayer;
                default: return string.Empty;
            }
        }
    }
}
