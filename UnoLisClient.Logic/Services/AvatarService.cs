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

            try
            {
                var callbackHandler = new AvatarCallback(response =>
                {
                    taskCompletion.TrySetResult(response);
                    CloseClientHelper.CloseClient(avatarClient);
                }, null);
                var context = new InstanceContext(callbackHandler);
                avatarClient = new AvatarManagerClient(context);
                avatarClient.GetPlayerAvatars(nickname);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (GetAvatar): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (GetAvatar): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación durante la obtención de avatares.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado durante la obtención de avatares.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(avatarClient);
            }
            return taskCompletion.Task;
        }

        public Task<ServiceResponse<object>> SetAvatarAsync(string nickname, int avatarId)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            AvatarManagerClient avatarClient = null;

            try
            {
                var callbackHandler = new AvatarCallback(null, response =>
                {
                    taskCompletion.TrySetResult(response);
                    CloseClientHelper.CloseClient(avatarClient);
                });
                var context = new InstanceContext(callbackHandler);
                avatarClient = new AvatarManagerClient(context);
                avatarClient.SetPlayerAvatar(nickname, avatarId);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (SetAvatar): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (SetAvatar): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación durante la actualización de avatar.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(avatarClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado durante la actualización de avatar.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(avatarClient);
            }
            return taskCompletion.Task;
        }
    }
}
