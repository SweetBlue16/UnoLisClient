using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.ProfileEdit;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class ProfileEditService
    {
        public Task<ServiceResponse<ProfileData>> UpdateProfileAsync(ProfileData profileData)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<ProfileData>>();
            ProfileEditManagerClient profileEditClient = null;

            Action<ServiceResponse<ProfileData>> callbackAction = (response) =>
            {
                try
                {
                    taskCompletion.TrySetResult(response);
                }
                finally
                {
                    CloseClientHelper.CloseClient(profileEditClient);
                }
            };
            var callbackHandler = new ProfileEditCallback(callbackAction, null);
            var context = new InstanceContext(callbackHandler);
            profileEditClient = new ProfileEditManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => profileEditClient.UpdateProfileData(profileData),
                taskCompletionSource: taskCompletion,
                client: profileEditClient,
                operationName: "UpdateProfile"
            );

            return taskCompletion.Task;
        }

        public Task<ServiceResponse<object>> RequestVerificationCodeAsync(string nickname, string newEmail)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            ProfileEditManagerClient profileEditClient = null;
            Action<ServiceResponse<object>> callbackAction = (response) =>
            {
                try
                {
                    taskCompletion.TrySetResult(response);
                }
                finally
                {
                    CloseClientHelper.CloseClient(profileEditClient);
                }
            };
            var callbackHandler = new ProfileEditCallback(null, callbackAction);
            var context = new InstanceContext(callbackHandler);
            profileEditClient = new ProfileEditManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => profileEditClient.RequestEmailChangeVerification(nickname, newEmail),
                taskCompletionSource: taskCompletion,
                client: profileEditClient,
                operationName: "RequestEmailChangeVerification"
            );
            return taskCompletion.Task;
        }
    }
}
