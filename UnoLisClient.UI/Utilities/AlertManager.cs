using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Utilities
{
    public class AlertManager : IDialogService
    {
        private LoadingPopUpWindow _loadingPopUpWindow;

        public string HandleValidationErrors(List<string> validationErrors)
        {
            string message = "◆ " + string.Join("\n◆ ", validationErrors);
            ShowAlert(Global.WarningLabel, message, PopUpIconType.Warning);
            return message;
        }

        public void HideLoading()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                _loadingPopUpWindow = null;
            }));
        }

        public void ShowAlert(string title, string message, PopUpIconType icon = PopUpIconType.None)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                new SimplePopUpWindow(title, message, icon).ShowDialog();
            }));
        }

        public string ShowInputDialog(string title, string message, string placeholder, PopUpIconType icon = PopUpIconType.None)
        {
            var inputPopUp = new InputPopUpWindow(title, message, placeholder, icon);

            if (inputPopUp.ShowDialog() == true)
            {
                return inputPopUp.UserInput;
            }
            return string.Empty;
        }

        public void ShowLoading(Page page)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_loadingPopUpWindow == null || !_loadingPopUpWindow.IsVisible)
                {
                    _loadingPopUpWindow = new LoadingPopUpWindow { Owner = GetOwnerWindow(page) };
                    _loadingPopUpWindow.Show();
                }
            }));
        }

        public bool ShowQuestionDialog(string title, string question, PopUpIconType icon = PopUpIconType.None)
        {
            var questionPopUp = new QuestionPopUpWindow(title, question, icon);
            return questionPopUp.ShowDialog() == true;
        }

        public void ShowWarning(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ShowAlert(Global.WarningLabel, message, PopUpIconType.Warning);
            }));
        }

        private static Window GetOwnerWindow(Page page)
        {
            return Window.GetWindow(page);
        }
    }
}
