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

            string imageName = string.Empty;
            switch (iconType)
            {
                case PopUpIconType.Success: imageName = "OkAlert.png"; break;
                case PopUpIconType.Error: imageName = "ErrorAlert.png"; break;
                case PopUpIconType.Warning: imageName = "WarningAlert.png"; break;
                case PopUpIconType.Info: imageName = "Info.png"; break;
                case PopUpIconType.Question: imageName = "QuestionAlert.png"; break;
                case PopUpIconType.Logout: imageName = "Logout.png"; break;
                case PopUpIconType.Login: imageName = "Login.png"; break;
                case PopUpIconType.EmailVerification: imageName = "EmailVerification.png"; break;
                case PopUpIconType.CreateAccount: imageName = "CreateAccount.png"; break;
                case PopUpIconType.RecoverPassword: imageName = "RecoverPassword.png"; break;
                case PopUpIconType.ReportPlayer: imageName = "ReportPlayer.png"; break;
                default: return null;
            }

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
    }
}
