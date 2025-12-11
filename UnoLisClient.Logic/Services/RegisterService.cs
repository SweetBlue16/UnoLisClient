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


            WcfServiceHelper.ExecuteSafe(
                action: () => registerClient.Register(data),
                taskCompletionSource: taskCompletion,
                client: registerClient,
                operationName: "UpdateProfile"
            );
            return taskCompletion.Task;
        }
    }
}
