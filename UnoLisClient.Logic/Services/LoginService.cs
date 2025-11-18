using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class LoginService
    {
        public Task<ServiceResponse<object>> LoginAsync(AuthCredentials credentials)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            LoginManagerClient loginClient = null;

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
                        CloseClientHelper.CloseClient(loginClient);
                    }
                };
                var callbackHandler = new LoginCallback(callbackAction);
                var context = new InstanceContext(callbackHandler);
                loginClient = new LoginManagerClient(context);

                loginClient.Login(credentials);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (Login): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(loginClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (Login): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(loginClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación durante el inicio de sesión.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(loginClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado durante el inicio de sesión.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(loginClient);
            }
            return taskCompletion.Task;
        }
    }
}