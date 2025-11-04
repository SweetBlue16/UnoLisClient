using System;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.UnoLisServerReference.Logout;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;
using UnoLisClient.Logic.Helpers;

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
            if (response.Success)
            {
                LogManager.Info("Servidor confirmó el cierre de sesión de OnExit.");
            }
            else
            {
                string message = MessageTranslator.GetMessage(response.Code);
                LogManager.Warn($"Servidor reportó un error al cerrar sesión de OnExit: {message}");
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LogManager.Info("🎮 UNO LIS Client iniciado.");

            var langCode = UI.Properties.Settings.Default.languageCode;
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
            ExecuteExitLogout();
        }

        private void ExecuteExitLogout()
        {
            try
            {
                string nickname = CurrentSession.CurrentUserNickname;
                if (string.IsNullOrWhiteSpace(nickname) || nickname == "Guest")
                {
                    return;
                }
                ClearLocalSession();

                var context = new InstanceContext(this);
                _logoutClient = new LogoutManagerClient(context);
                _logoutClient.LogoutAsync(nickname);
            }
            catch (CommunicationException ex)
            {
                LogManager.Error("Error de comunicación al notificar logout en OnExit.", ex);
                CloseClientHelper.CloseClient(_logoutClient);
            }
            catch (Exception ex)
            {
                LogManager.Error($"Error inesperado en ExecuteExitLogout: {ex.Message}", ex);
                CloseClientHelper.CloseClient(_logoutClient);
            }
        }

        private void ClearLocalSession()
        {
            CurrentSession.CurrentUserNickname = null;
            CurrentSession.CurrentUserProfileData = null;
        }
    }
}
