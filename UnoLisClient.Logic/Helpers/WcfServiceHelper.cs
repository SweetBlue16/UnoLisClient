using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UnoLisClient.Logic.Helpers
{
    public static class WcfServiceHelper
    {
        /// <summary>
        /// Executes a WCF action safely.
        /// Manages common WCF exceptions and closes the client if needed.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="action">WCF acction executing (ej. client.GetData()).</param>
        /// <param name="taskCompletionSource">TaskCompletionSource expecting response.</param>
        /// <param name="client">WCF Client (proxy).</param>
        /// <param name="operationName">OperationName for logs.</param>
        public static void ExecuteSafe<T>(Action action, TaskCompletionSource<T> taskCompletionSource,
            ICommunicationObject client, string operationName)
        {
            try
            {
                action();
            }
            catch (EndpointNotFoundException ex)
            {
                Logger.Error($"Connection error ({operationName}): Endponit not found.", ex);
                taskCompletionSource.TrySetException(ex);
                CloseClientHelper.CloseClient(client);
            }
            catch (TimeoutException ex)
            {
                Logger.Error($"Connection Error ({operationName}): Waiting time exceeded.", ex);
                taskCompletionSource.TrySetException(ex);
                CloseClientHelper.CloseClient(client);
            }
            catch (CommunicationException ex)
            {
                Logger.Error($"Communication Error in ({operationName}).", ex);
                taskCompletionSource.TrySetException(ex);
                CloseClientHelper.CloseClient(client);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected Error in ({operationName}).", ex);
                taskCompletionSource.TrySetException(ex);
                CloseClientHelper.CloseClient(client);
            }
        }
    }
}
