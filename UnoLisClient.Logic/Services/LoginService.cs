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

            Action<ServiceResponse<object>> callbackAction = (response) =>
            {
                taskCompletion.TrySetResult(response);
                CloseClientHelper.CloseClient(loginClient);
            };

            var callbackHandler = new LoginCallback(callbackAction);
            var context = new InstanceContext(callbackHandler);
            loginClient = new LoginManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                action: () => loginClient.Login(credentials),
                taskCompletionSource: taskCompletion,
                client: loginClient,
                operationName: "Login"
            );

            return taskCompletion.Task;
        }
    }
}