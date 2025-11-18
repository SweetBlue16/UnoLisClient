using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Avatar;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class AvatarCallback : IAvatarManagerCallback
    {
        private readonly Action<ServiceResponse<PlayerAvatar[]>> _onAvatarsDataReceived;
        private readonly Action<ServiceResponse<object>> _onAvatarUpdateResponse;

        public AvatarCallback(Action<ServiceResponse<PlayerAvatar[]>> onAvatarDataReceived,
            Action<ServiceResponse<object>> onAvatarUpdateResponse)
        {
            _onAvatarsDataReceived = onAvatarDataReceived;
            _onAvatarUpdateResponse = onAvatarUpdateResponse;
        }

        public void AvatarsDataReceived(ServiceResponse<PlayerAvatar[]> response)
        {
            Application.Current?.Dispatcher.Invoke(() =>
                _onAvatarsDataReceived?.Invoke(response)
            );
        }

        public void AvatarUpdateResponse(ServiceResponse<object> response)
        {
            Application.Current?.Dispatcher.Invoke(() =>
                _onAvatarUpdateResponse?.Invoke(response)
            );
        }
    }
}
