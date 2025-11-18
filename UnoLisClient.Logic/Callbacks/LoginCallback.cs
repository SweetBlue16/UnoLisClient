using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class LoginCallback : ILoginManagerCallback
    {
        private readonly Action<ServiceResponse<object>> _onResponseCallback;
        public LoginCallback(Action<ServiceResponse<object>> onResponseCallback)
        {
            _onResponseCallback = onResponseCallback;
        }

        public void LoginResponse(ServiceResponse<object> response)
        {
            if (Application.Current == null)
                return;
            
            Application.Current.Dispatcher.Invoke(() =>
                _onResponseCallback?.Invoke(response)
            );
        }

    }
}