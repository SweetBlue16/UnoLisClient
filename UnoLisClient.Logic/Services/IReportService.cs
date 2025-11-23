using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public interface IReportService
    {
        Task<ServiceResponse<object>> ReportPlayerAsync(ReportData reportData);
    }

    public class ReportService : IReportService
    {
        public Task<ServiceResponse<object>> ReportPlayerAsync(ReportData reportData)
        {
            Console.WriteLine($"[DEBUG] Iniciando ReportPlayerAsync para: {reportData.ReportedNickname}");
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();
            ReportManagerClient reportManagerClient = null;

            Action<ServiceResponse<object>> callbackAction = (response) =>
            {
                try
                {
                    taskCompletion.TrySetResult(response);
                }
                finally
                {
                    CloseClientHelper.CloseClient(reportManagerClient);
                }
            };

            var callbackHandler = new ReportCallback(callbackAction);
            var context = new InstanceContext(callbackHandler);
            reportManagerClient = new ReportManagerClient(context);

            WcfServiceHelper.ExecuteSafe(
                    action: () => reportManagerClient.ReportPlayer(reportData), taskCompletionSource: taskCompletion,
                    client: reportManagerClient, operationName: "ReportPlayer"
                );
            return taskCompletion.Task;
        }
    }
}
