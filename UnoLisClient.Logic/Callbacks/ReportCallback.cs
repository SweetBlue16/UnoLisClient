using System;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ReportCallback : IReportManagerCallback
    {
        public static event Action<ServiceResponse<object>> OnResponse;

        public ReportCallback(Action<ServiceResponse<object>> onResponse)
        {
            OnResponse = onResponse;
        }

        public void OnPlayerKicked(ServiceResponse<object> response)
        {
            Console.WriteLine($"[DEBUG] Callback recibido - Success: {response.Success}");
            try
            {
                OnResponse?.Invoke(response);
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
                OnResponse?.Invoke(response);
                Console.WriteLine($"[DEBUG] Callback completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] En callback: {ex.Message}");
            }
        }
    }
}
