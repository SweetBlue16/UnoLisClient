using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Register;
using UnoLisClient.Logic.Callbacks;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class RegisterService
    {
        public Task<ServiceResponse<object>> RegisterAsync(RegistrationData data)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            RegisterManagerClient registerClient = null;

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
                        CloseClientHelper.CloseClient(registerClient);
                    }
                };
                var callbackHandler = new RegisterCallback(callbackAction);
                var context = new InstanceContext(callbackHandler);
                registerClient = new RegisterManagerClient(context);

                registerClient.Register(data);
            }
            catch (EndpointNotFoundException enfEx)
            {
                LogManager.Error("Error de conexión (Register): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(registerClient);
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error("Error de conexión (Register): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(registerClient);
            }
            catch (CommunicationException commEx)
            {
                LogManager.Error("Error de comunicación durante el registro.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(registerClient);
            }
            catch (Exception ex)
            {
                LogManager.Error("Error inesperado durante el registro.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(registerClient);
            }
            return taskCompletion.Task;
        }
    }
}
