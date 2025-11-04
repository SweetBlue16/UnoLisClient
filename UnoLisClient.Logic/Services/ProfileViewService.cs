using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class ProfileViewService
    {
        public Task<ServiceResponse<ProfileData>> GetProfileDataAsync(string nickname)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<ProfileData>>();
            ProfileViewManagerClient profileViewClient = null;

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
                        CloseClientHelper.CloseClient(profileViewClient);
                    }
                };
                var callbackHandler = new ProfileViewCallback(callbackAction);
                var context = new InstanceContext(callbackHandler);
                profileViewClient = new ProfileViewManagerClient(context);

                profileViewClient.GetProfileData(nickname);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (ProfileView): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(profileViewClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (ProfileView): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(profileViewClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación al obtener los datos del perfil.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(profileViewClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado al obtener los datos del perfil.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(profileViewClient);
            }
            return taskCompletion.Task;
        }
    }
}
