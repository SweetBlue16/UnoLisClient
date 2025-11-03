using System;
using System.ServiceModel;

namespace UnoLisClient.Logic.Helpers
{
    public static class CloseClientHelper
    {
        public static void CloseClient(ICommunicationObject client)
        {
            if (client == null)
            {
                return;
            }

            if (client.State == CommunicationState.Faulted)
            {
                client.Abort();
                return;
            }

            try
            {
                client.Close();
            }
            catch (CommunicationException communicationEx)
            {
                LogManager.Error($"Fallo 'CommunicationException' al cerrar el cliente. Abortando canal.", communicationEx);
                client.Abort();
            }
            catch (TimeoutException timeoutEx)
            {
                LogManager.Error($"Fallo 'TimeoutException' al cerrar el cliente. Abortando canal.", timeoutEx);
                client.Abort();
            }
            catch (Exception ex)
            {
                LogManager.Error($"Fallo desconocido al cerrar el cliente. Abortando canal.", ex);
                client.Abort();
            }
        }
    }
}
