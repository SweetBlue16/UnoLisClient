using System;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ReportCallback : IReportManagerCallback
    {
        private readonly Action<ServiceResponse<object>> _onResponse;

        public ReportCallback(Action<ServiceResponse<object>> onResponse)
        {
            _onResponse = onResponse;
        }

        public void ReportPlayerResponse(ServiceResponse<object> response)
        {
            Console.WriteLine($"[DEBUG] Callback recibido - Success: {response.Success}");
            try
            {
                _onResponse?.Invoke(response);
                Console.WriteLine($"[DEBUG] Callback completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] En callback: {ex.Message}");
            }
        }
    }
}
