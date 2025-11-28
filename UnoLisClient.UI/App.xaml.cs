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
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisClient.UI.Views.UnoLisWindows;
using UnoLisServer.Common.Enums;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.Logic.Enums;
using System.Linq;
using UnoLisClient.UI.Views.UnoLisPages;

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
            BanSessionManager.PlayerBanned += HandleGlobalBan;

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

        private void HandleGlobalBan(BanInfo banInfo)
        {
            Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Current.Windows.OfType<MainWindow>().FirstOrDefault();
                var openWindows = Current.Windows.OfType<Window>().ToList();
                foreach (var window in openWindows)
                {
                    if (window.GetType() != typeof(MainWindow))
                    {
                        window.Close();
                    }
                }

                string message = string.Format(MessageTranslator.GetMessage(MessageCode.SanctionApplied), banInfo.FormattedTimeRemaining);
                new SimplePopUpWindow(Global.OopsLabel, message, PopUpIconType.Warning).ShowDialog();

                if (mainWindow != null)
                {
                    if (mainWindow.WindowState == WindowState.Minimized)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                    }
                    mainWindow.Activate();
                    mainWindow.NavigateToInitialPageAndClearHistory(new GamePage());
                }
                else
                {
                    var newTitlePage = new MainWindow();
                    newTitlePage.Show();
                }
                ClearLocalSession();
            });
        }
    }
}
