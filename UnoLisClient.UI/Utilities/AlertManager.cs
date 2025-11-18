using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            ShowAlert(Global.WarningLabel, message);
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

        public void ShowAlert(string title, string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                new SimplePopUpWindow(title, message).ShowDialog();
            }));
        }

        public string ShowInputDialog(string title, string message, string placeholder)
        {
            var inputPopUp = new InputPopUpWindow(title, message, placeholder);

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

        public bool ShowQuestionDialog(string title, string question)
        {
            var questionPopUp = new QuestionPopUpWindow(title, question);
            return questionPopUp.ShowDialog() == true;
        }

        public void ShowWarning(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ShowAlert(Global.WarningLabel, message);
            }));
        }

        private Window GetOwnerWindow(Page page)
        {
            return Window.GetWindow(page);
        }
    }
}
