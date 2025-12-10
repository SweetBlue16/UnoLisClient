using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisServer.Common.Enums;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class ProfileViewService : IProfileViewService
    {
        public Task<ServiceResponse<ProfileData>> GetProfileDataAsync(string nickname)
        {
            Console.WriteLine($"[DEBUG] Iniciando GetProfileDataAsync para: {nickname}");
            var taskCompletion = new TaskCompletionSource<ServiceResponse<ProfileData>>();
            ProfileViewManagerClient profileViewClient = null;

            Action<ServiceResponse<ProfileData>> callbackAction = (response) =>
            {
                try
                {
                    taskCompletion.TrySetResult(response);
                }
                finally
                {
                    CloseClientHelper.CloseClient(profileViewClient);
                }
            };

            var callbackHandler = new ProfileViewCallback(callbackAction);
            var context = new InstanceContext(callbackHandler);
            profileViewClient = new ProfileViewManagerClient(context);
            WcfServiceHelper.ExecuteSafe(
                action: () => profileViewClient.GetProfileData(nickname),taskCompletionSource: taskCompletion,
                client: profileViewClient, operationName: "ProfileView"
            );
            return taskCompletion.Task;
        }
    }
}
