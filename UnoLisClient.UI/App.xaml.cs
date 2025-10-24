using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.UnoLisServerReference.Logout;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ILogoutManagerCallback
    {
        private LogoutManagerClient _logoutClient;

        public void LogoutResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string message = MessageTranslator.GetMessage(response.Code);
                if (!response.Success)
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UnoLisClient.UI.Utilities.LogManager.Info("🎮 UNO LIS Client iniciado.");

            var langCode = global::UnoLisClient.UI.Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(langCode))
            {
                langCode = "en-US";
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(langCode);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            LogoutCurrentUser();
        }

        private void LogoutCurrentUser()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname))
                {
                    var context = new InstanceContext(this);
                    _logoutClient = new LogoutManagerClient(context);
                    _logoutClient.LogoutAsync(CurrentSession.CurrentUserNickname);

                    CurrentSession.CurrentUserNickname = null;
                    CurrentSession.CurrentUserProfileData = null;
                }
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }
    }
}
