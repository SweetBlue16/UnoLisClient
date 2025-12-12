using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Logout;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.UI.Views.UnoLisWindows;
using UnoLisServer.Common.Enums;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ILogoutManagerCallback
    {
        public App()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private LogoutManagerClient _logoutClient;

        public void LogoutResponse(ServiceResponse<object> response)
        {
            if (response.Success)
            {
                Logger.Info("Servidor confirmó el cierre de sesión de OnExit.");
            }
            else
            {
                string message = MessageTranslator.GetMessage(response.Code);
                Logger.Warn($"Servidor reportó un error al cerrar sesión de OnExit: {message}");
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BanSessionManager.PlayerBanned += OnGlobalPlayerBanned;

            Logger.Info("🎮 UNO LIS Client iniciado.");

            var langCode = UI.Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(langCode))
            {
                langCode = "en-US";
            }

            ServerConnectionMonitor.OnServerConnectionLost = (messageKey) =>
            {
                NavigationProxy.ForceLogoutToLogin(messageKey);
            };

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
                SessionReportTracker.Clear();

                var context = new InstanceContext(this);
                _logoutClient = new LogoutManagerClient(context);
                _logoutClient.LogoutAsync(nickname);
            }
            catch (CommunicationException ex)
            {
                Logger.Error("Error de comunicación al notificar logout en OnExit.", ex);
                CloseClientHelper.CloseClient(_logoutClient);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error inesperado en ExecuteExitLogout: {ex.Message}", ex);
                CloseClientHelper.CloseClient(_logoutClient);
            }
        }

        private void ClearLocalSession()
        {
            CurrentSession.CurrentUserNickname = null;
            CurrentSession.CurrentUserProfileData = null;
        }

        private void OnGlobalPlayerBanned(BanInfo banInfo)
        {
            Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Current.Windows.OfType<MainWindow>().FirstOrDefault();
                var windowsToClose = Current.Windows.OfType<Window>()
                    .Where(w => w != mainWindow)
                    .ToList();
                foreach (var window in windowsToClose)
                {
                    window.Close();
                }

                string timeRemaining = banInfo?.FormattedTimeRemaining;
                string banMessage = string.Format(
                    MessageTranslator.GetMessage(MessageCode.PlayerBanned),
                    timeRemaining);
                new SimplePopUpWindow(Global.OopsLabel, banMessage, PopUpIconType.Error).ShowDialog();

                if (mainWindow != null)
                {
                    if (mainWindow.WindowState == WindowState.Minimized)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                    }
                    mainWindow.Activate();
                    if (mainWindow.FindName("MainFrame") is Frame frame)
                    {
                        frame.Navigate(new GamePage());
                        while (frame.CanGoBack)
                        {
                            frame.RemoveBackEntry();
                        }
                    }
                }
                else
                {
                    new MainWindow().Show();
                }
                ExecuteExitLogout();
            });
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error("CRASH AVOIDED (UI): Exception occured in the interface.", e.Exception);

            string errorMessage = $"An unexpected error occurred: {e.Exception.Message}\n\nThe application will try to continue.";

            MessageBox.Show(errorMessage, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Logger.Error("CRITICAL CRASH (UI). Exception occured in secondary thread", ex);
        }
    }
}
