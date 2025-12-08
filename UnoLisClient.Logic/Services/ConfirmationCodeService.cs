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

            var callbackHandler = new ConfirmationCodeCallback(response =>
            {
                taskCompletion.TrySetResult(response);
                CloseClientHelper.CloseClient(confirmationClient);
            }, null);

            var context = new InstanceContext(callbackHandler);
            confirmationClient = new ConfirmationManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => confirmationClient.ConfirmCode(email, code),
                taskCompletionSource: taskCompletion,
                client: confirmationClient,
                operationName: "ConfirmCode"
            );

            return taskCompletion.Task;
        }

        public Task<ServiceResponse<object>> ResendCodeAsync(string email)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            ConfirmationManagerClient confirmationClient = null;

            var callbackHandler = new ConfirmationCodeCallback(null, response =>
            {
                taskCompletion.TrySetResult(response);
                CloseClientHelper.CloseClient(confirmationClient);
            });

            var context = new InstanceContext(callbackHandler);
            confirmationClient = new ConfirmationManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => confirmationClient.ResendConfirmationCode(email),
                taskCompletionSource: taskCompletion,
                client: confirmationClient,
                operationName: "ResendCode"
            );

            return taskCompletion.Task;
        }
    }
}
