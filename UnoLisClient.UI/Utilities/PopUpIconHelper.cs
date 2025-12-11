using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnoLisClient.Logic.Enums;

namespace UnoLisClient.UI.Utilities
{
    public static class PopUpIconHelper
    {
        public static ImageSource GetIconSource(PopUpIconType iconType)
        {
            if (iconType == PopUpIconType.None)
            {
                return null;
            }

            string imageName = GetImagePath(iconType);

            if (string.IsNullOrEmpty(imageName))
            {
                return null;
            }

            try
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Assets/Other/{imageName}"));
            }
            catch
            {
                return null;
            }
        }

        private static string GetImagePath(PopUpIconType iconType)
        {
            switch (iconType)
            {
                case PopUpIconType.Success: return "OkAlert.png";
                case PopUpIconType.Error: return "ErrorAlert.png";
                case PopUpIconType.Warning: return "WarningAlert.png";
                case PopUpIconType.Info: return "Info.png";
                case PopUpIconType.Question: return "QuestionAlert.png";
                case PopUpIconType.Logout: return "Logout.png";
                case PopUpIconType.Login: return "Login.png";
                case PopUpIconType.EmailVerification: return "EmailVerification.png";
                case PopUpIconType.CreateAccount: return "CreateAccount.png";
                case PopUpIconType.RecoverPassword: return "RecoverPassword.png";
                case PopUpIconType.ReportPlayer: return "ReportPlayer.png";
                default: return string.Empty;
            }
        }
    }
}
