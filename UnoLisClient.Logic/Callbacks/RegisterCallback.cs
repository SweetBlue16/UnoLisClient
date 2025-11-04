using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Register;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class RegisterCallback : IRegisterManagerCallback
    {
        private readonly Action<ServiceResponse<object>> _onRegisterResponse;

        public RegisterCallback(Action<ServiceResponse<object>> onRegisterResponse)
        {
            _onRegisterResponse = onRegisterResponse;
        }

        public void RegisterResponse(ServiceResponse<object> response)
        {
            Application.Current?.Dispatcher.Invoke(() =>
                _onRegisterResponse?.Invoke(response)
            );
        }
    }
}
