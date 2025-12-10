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
        private readonly Action<ServiceResponse<object>> _onEmailCodeSentResponse;

        public ProfileEditCallback(Action<ServiceResponse<ProfileData>> onResponse, Action<ServiceResponse<object>> onEmailCodeSentResponse)
        {
            _onResponse = onResponse;
            _onEmailCodeSentResponse = onEmailCodeSentResponse;
        }

        public void EmailChangeVerificationCodeSentResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _onEmailCodeSentResponse?.Invoke(response);
            });
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
