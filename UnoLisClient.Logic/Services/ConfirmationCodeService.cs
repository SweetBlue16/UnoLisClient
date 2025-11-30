using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Confirmation;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public class ConfirmationCodeService
    {
        public Task<ServiceResponse<object>> ConfirmCodeAsync(string email, string code)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            ConfirmationManagerClient confirmationClient = null;

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
                        CloseClientHelper.CloseClient(confirmationClient);
                    }
                };
                var callbackHandler = new ConfirmationCodeCallback(callbackAction, null);
                var context = new InstanceContext(callbackHandler);
                confirmationClient = new ConfirmationManagerClient(context);

                confirmationClient.ConfirmCode(email, code);
            }
            catch (EndpointNotFoundException enfEx)
            {
                Logger.Error("Error de conexión (ConfirmCode): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (TimeoutException timeoutEx)
            {
                Logger.Error("Error de conexión (ConfirmCode): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (CommunicationException commEx)
            {
                Logger.Error("Error de comunicación durante la confirmación de código de verificación.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (Exception ex)
            {
                Logger.Error("Error inesperado durante la confirmación de código de verificación.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            return taskCompletion.Task;
        }

        public Task<ServiceResponse<object>> ResendCodeAsync(string email)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            ConfirmationManagerClient confirmationClient = null;

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
                        CloseClientHelper.CloseClient(confirmationClient);
                    }
                };
                var callbackHandler = new ConfirmationCodeCallback(null, callbackAction);
                var context = new InstanceContext(callbackHandler);
                confirmationClient = new ConfirmationManagerClient(context);

                confirmationClient.ResendConfirmationCode(email);
            }
            catch (EndpointNotFoundException enfEx)
            {
                Logger.Error("Error de conexión (ResendCode): No se encontró el endpoint.", enfEx);
                taskCompletion.TrySetException(enfEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (TimeoutException timeoutEx)
            {
                Logger.Error("Error de conexión (ResendCode): Tiempo de espera agotado.", timeoutEx);
                taskCompletion.TrySetException(timeoutEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (CommunicationException commEx)
            {
                Logger.Error("Error de comunicación durante la confirmación de código de verificación.", commEx);
                taskCompletion.TrySetException(commEx);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            catch (Exception ex)
            {
                Logger.Error("Error inesperado durante la confirmación de código de verificación.", ex);
                taskCompletion.TrySetException(ex);
                CloseClientHelper.CloseClient(confirmationClient);
            }
            return taskCompletion.Task;
        }
    }
}
