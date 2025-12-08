using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Avatar;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class AvatarService
    {
        public Task<ServiceResponse<PlayerAvatar[]>> GetAvatarsAsync(string nickname)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<PlayerAvatar[]>>();
            AvatarManagerClient avatarClient = null;


            var callbackHandler = new AvatarCallback(response =>
            {
                taskCompletion.TrySetResult(response);
                CloseClientHelper.CloseClient(avatarClient);
            }, null);

            var context = new InstanceContext(callbackHandler);
            avatarClient = new AvatarManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => avatarClient.GetPlayerAvatars(nickname),
                taskCompletionSource: taskCompletion,
                client: avatarClient,
                operationName: "GetAvatars"
            );

            return taskCompletion.Task;
        }

        public Task<ServiceResponse<object>> SetAvatarAsync(string nickname, int avatarId)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            AvatarManagerClient avatarClient = null;


            var callbackHandler = new AvatarCallback(null, response =>
            {
                taskCompletion.TrySetResult(response);
                CloseClientHelper.CloseClient(avatarClient);
            });
            var context = new InstanceContext(callbackHandler);
            avatarClient = new AvatarManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => avatarClient.SetPlayerAvatar(nickname, avatarId),
                taskCompletionSource: taskCompletion,
                client: avatarClient,
                operationName: "GetAvatars"
            );

            return taskCompletion.Task;
        }
    }
}
