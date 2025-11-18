using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Logout;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class LogoutCallback : ILogoutManagerCallback
    {
        private readonly Action<ServiceResponse<object>> _onLogoutResponse;

        public LogoutCallback(Action<ServiceResponse<object>> onLogoutResponse)
        {
            _onLogoutResponse = onLogoutResponse;
        }

        public void LogoutResponse(ServiceResponse<object> response)
        {
            Application.Current?.Dispatcher.Invoke(() =>
                _onLogoutResponse?.Invoke(response)
            );
        }
    }
}
