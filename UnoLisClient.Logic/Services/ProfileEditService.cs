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
            var callbackHandler = new ProfileEditCallback(callbackAction);
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
    }
}
