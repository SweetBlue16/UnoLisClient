using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisServer.Common.Enums;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Helpers
{
    public class SafeReportManagerClient : IDisposable
    {
        private readonly ReportManagerClient _client;
        private readonly ReportCallback _callback;
        private bool _disposed;

        public ReportManagerClient InnerClient => _client;

        public SafeReportManagerClient(ReportCallback callbackHandler)
        {
            _callback = callbackHandler ?? throw new ArgumentNullException(nameof(callbackHandler));

            var context = new InstanceContext(_callback);
            _client = new ReportManagerClient(context);
        }

        public async Task<ServiceResponse<object>> ReportPlayerAsync(ReportData reportData)
        {
            var taskCompletion = new TaskCompletionSource<ServiceResponse<object>>();

            void Handler(ServiceResponse<object> resp) => taskCompletion.TrySetResult(resp);

            _callback.OnReportResponse += Handler;

            try
            {
                await Task.Run(() => _client.ReportPlayer(reportData));
                return await taskCompletion.Task;
            }
            catch (EndpointNotFoundException)
            {
                return CreateFail(MessageCode.ConnectionRejected);
            }
            catch (TimeoutException)
            {
                return CreateFail(MessageCode.Timeout);
            }
            catch (CommunicationException)
            {
                return CreateFail(MessageCode.ConnectionFailed);
            }
            catch (Exception)
            {
                return CreateFail(MessageCode.UnhandledException);
            }
            finally
            {
                _callback.OnReportResponse -= Handler;
            }
        }

        public void ReportPlayer(ReportData reportData)
        {
            _client.ReportPlayer(reportData);
        }

        private ServiceResponse<object> CreateFail(MessageCode code) =>
        new ServiceResponse<object>
        {
            Success = false,
            Code = code
        };

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            CloseClientHelper.CloseClient(_client);
        }
    }
}