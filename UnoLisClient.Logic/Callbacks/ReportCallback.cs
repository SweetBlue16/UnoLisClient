using System;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ReportCallback : IReportManagerCallback
    {
        public event Action<ServiceResponse<object>> OnReportResponse;
        public event Action<ServiceResponse<object>> OnPlayerKickedResponse;

        public void OnPlayerKicked(ServiceResponse<object> response)
        {
            Console.WriteLine($"[DEBUG] Callback recibido - Success: {response.Success}");
            try
            {
                OnPlayerKickedResponse?.Invoke(response);
                Console.WriteLine($"[DEBUG] Callback completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] En callback: {ex.Message}");
            }
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
