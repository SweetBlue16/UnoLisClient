using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Logout;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public interface ILogoutService
    {
        Task<ServiceResponse<object>> LogoutAsync(string nickname);
    }
    public class LogoutService : ILogoutService
    {
        public Task<ServiceResponse<object>> LogoutAsync(string nickname)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            LogoutManagerClient logoutClient = null;


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

            WcfServiceHelper.ExecuteSafe(
                action: () => logoutClient.Logout(nickname), taskCompletionSource: taskCompletion, 
                client: logoutClient, operationName: "Logout"
            );

            return taskCompletion.Task;
        }
    }
}
