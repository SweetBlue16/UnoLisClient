using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Logout;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class LogoutService
    {
        public Task<ServiceResponse<object>> LogoutAsync(string nickname)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            LogoutManagerClient logoutClient = null;

            try
            {
                Action<ServiceResponse<object>> callbackAction = (response) =>
                {
                    try
                    {
                        taskCompletion.TrySetResult(response);
                    }
                    finally
                    {
                        CloseClientHelper.CloseClient(logoutClient);
                    }
                };
                var callbackHandler = new LogoutCallback(callbackAction);
                var context = new InstanceContext(callbackHandler);
                logoutClient = new LogoutManagerClient(context);

                logoutClient.Logout(nickname);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (Logout): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(logoutClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (Logout): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(logoutClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación durante el cierre de sesión.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(logoutClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado durante el cierre de sesión.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(logoutClient);
            }
            return taskCompletion.Task;
        }
    }
}
