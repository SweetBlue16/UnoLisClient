using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.ProfileEdit;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ProfileEditCallback : IProfileEditManagerCallback
    {
        private readonly Action<ServiceResponse<ProfileData>> _onResponse;

        public ProfileEditCallback(Action<ServiceResponse<ProfileData>> onResponse)
        {
            _onResponse = onResponse;
        }

        public void ProfileUpdateResponse(ServiceResponse<ProfileData> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _onResponse?.Invoke(response);
            });
        }
    }
}
