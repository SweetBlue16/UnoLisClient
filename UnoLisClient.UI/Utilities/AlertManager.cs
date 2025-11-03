using System;
using System.Collections.Generic;
using System.Windows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Utilities
{
    public class AlertManager : IDialogService
    {
        private LoadingPopUpWindow _loadingPopUpWindow;

        public void HandleValidationErrors(List<string> validationErrors, Window owner)
        {
            string message = "◆ " + string.Join("\n◆ ", validationErrors);
            ShowAlert(Global.WarningLabel, message, owner);
        }

        public void HideLoading()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                _loadingPopUpWindow = null;
            }));
        }

        public void ShowAlert(string title, string message, Window owner)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                new SimplePopUpWindow(title, message).ShowDialog();
            }));
        }

        public void ShowLoading(Window owner)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_loadingPopUpWindow == null || !_loadingPopUpWindow.IsVisible)
                {
                    _loadingPopUpWindow = new LoadingPopUpWindow { Owner = owner };
                    _loadingPopUpWindow.Show();
                }
            }));
        }

        public void ShowWarning(string message, Window owner)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ShowAlert(Global.WarningLabel, message, owner);
            }));
        }
    }
}
