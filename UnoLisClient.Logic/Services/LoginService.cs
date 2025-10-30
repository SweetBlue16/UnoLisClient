using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
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
                var callbackHandler = new LoginCallback(response =>
                {
                    taskCompletion.TrySetResult(response);
                    CloseClient(loginClient);
                });

                var context = new InstanceContext(callbackHandler);
                loginClient = new LoginManagerClient(context);

                loginClient.Login(credentials);
            }
            catch (Exception ex)
            {
                taskCompletion.TrySetException(ex);
                CloseClient(loginClient);
            }

            return taskCompletion.Task;
        }

        private void CloseClient(ICommunicationObject client)
        {
            if (client == null)
            {
                return;
            }

            try
            {
                if (client.State != CommunicationState.Faulted)
                {
                    client.Close();
                }
            }
            catch 
            { 
                client.Abort(); 
            }
        }
    }
}
