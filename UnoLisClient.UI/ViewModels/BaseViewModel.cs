using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;

            protected set => SetProperty(ref _isLoading, value);
        }

        protected readonly IDialogService _dialogService;

        public BaseViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        protected void HandleException(MessageCode code, string logMessage, Exception ex)
        {
            IsLoading = false;
            LogManager.Error(logMessage, ex);

            string userMessage = MessageTranslator.GetMessage(code);

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                _dialogService.ShowAlert(Global.UnsuccessfulLabel, userMessage, PopUpIconType.Error);
            }));
        }
    }
}
