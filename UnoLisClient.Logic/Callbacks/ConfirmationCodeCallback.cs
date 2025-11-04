using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Confirmation;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ConfirmationCodeCallback : IConfirmationManagerCallback
    {
        private readonly Action<ServiceResponse<object>> _onConfirmResponse;
        private readonly Action<ServiceResponse<object>> _onResendCodeResponse;

        public ConfirmationCodeCallback(
            Action<ServiceResponse<object>> onConfirmResponse = null,
            Action<ServiceResponse<object>> onResendCodeResponse = null)
        {
            _onConfirmResponse = onConfirmResponse;
            _onResendCodeResponse = onResendCodeResponse;
        }

        public void ConfirmationResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _onConfirmResponse?.Invoke(response);
            });
        }

        public void ResendCodeResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _onResendCodeResponse?.Invoke(response);
            });
        }
    }
}
