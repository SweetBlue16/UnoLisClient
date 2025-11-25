using System;
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
            SafeReportManagerClient reportManagerClient = null;
            var callbackHandler = new ReportCallback();
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();

            callbackHandler.OnReportResponse += (response) =>
            {
                taskCompletion.TrySetResult(response);
            };

            try
            {
                reportManagerClient = new SafeReportManagerClient(callbackHandler);
                WcfServiceHelper.ExecuteSafe(
                    action: () => reportManagerClient.ReportPlayer(reportData),
                    taskCompletionSource: taskCompletion,
                    client: reportManagerClient.InnerClient,
                    operationName: "ReportPlayer"
                );
                return taskCompletion.Task;
            }
            catch (Exception ex)
            {
                taskCompletion.TrySetException(ex);
                return taskCompletion.Task;
            }
        }
    }
}
