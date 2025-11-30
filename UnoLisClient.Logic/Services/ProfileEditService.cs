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

            try
            {
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

                profileEditClient.UpdateProfileData(profileData);
            }
            catch (EndpointNotFoundException enfEx)
            {
                Logger.Error("Error de conexión (ProfileEdit): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(profileEditClient);
            }
            catch (TimeoutException timeoutEx)
            {
                Logger.Error("Error de conexión (ProfileEdit): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(profileEditClient);
            }
            catch (CommunicationException commEx)
            {
                Logger.Error("Error de comunicación al actualizar los datos del perfil.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(profileEditClient);
            }
            catch (Exception ex)
            {
                Logger.Error("Error inesperado al actualizar los datos del perfil.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(profileEditClient);
            }
            return taskCompletion.Task;
        }
    }
}
