using System;
using System.ServiceModel;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ReportCallback : IReportManagerCallback
    {
        public event Action<ServiceResponse<object>> OnReportResponse;
        public event Action<ServiceResponse<object>> OnPlayerKickedResponse;

        public void OnPlayerBanned(ServiceResponse<BanInfo> response)
        {
            BanSessionManager.TriggerBan(response.Data as BanInfo);
        }

        public void ReportPlayerResponse(ServiceResponse<object> response)
        {
            Console.WriteLine($"[DEBUG] Callback recibido - Success: {response.Success}");
            try
            {
                OnReportResponse?.Invoke(response);
                Console.WriteLine($"[DEBUG] Callback completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] En callback: {ex.Message}");
            }
        }
    }
}
