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
            catch (CommunicationException commEx)
            {
                Console.WriteLine($"[ERROR] Comunicación fallida en ReportPlayerAsync: {commEx.Message}");
                taskCompletion.TrySetException(commEx);
                return taskCompletion.Task;
            }
            catch (TimeoutException timeoutEx)
            {
                Console.WriteLine($"[ERROR] Tiempo de espera agotado en ReportPlayerAsync: {timeoutEx.Message}");
                taskCompletion.TrySetException(timeoutEx);
                return taskCompletion.Task;
            }
            catch (Exception ex)
            {
                taskCompletion.TrySetException(ex);
                return taskCompletion.Task;
            }
        }
    }

    public class ReportSessionService
    {
        private static ReportSessionService _instance;
        public static ReportSessionService Instance => _instance ?? (_instance = new ReportSessionService());

        private ReportManagerClient _reportManagerClient;
        public bool IsConnected { get; private set; }

        public void Initialize(string nickname)
        {
            if (IsConnected)
            {
                return;
            }

            try
            {
                var context = new InstanceContext(new ReportCallback());
                _reportManagerClient = new ReportManagerClient(context);
                _reportManagerClient.SuscrbeToBanNotifications(nickname);
                IsConnected = true;
            }
            catch (CommunicationException commEx)
            {
                Console.WriteLine($"[ERROR] Comunicación fallida al inicializar " +
                    $"ReportSessionService: {commEx.Message}");
                IsConnected = false;
            }
            catch (TimeoutException timeoutEx)
            {
                Console.WriteLine($"[ERROR] Tiempo de espera agotado al inicializar " +
                    $"ReportSessionService: {timeoutEx.Message}");
                IsConnected = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] No se pudo inicializar ReportSessionService: {ex.Message}");
                IsConnected = false;
            }
        }

        public void SendReport(ReportData reportData)
        {
            if (!IsConnected)
            {
                Console.WriteLine("[WARNING] ReportSessionService no está conectado. No se puede enviar el reporte.");
                return;
            }

            try
            {
                _reportManagerClient.ReportPlayer(reportData);
                Console.WriteLine($"[DEBUG] Reporte enviado para: {reportData.ReportedNickname}");
            }
            catch (CommunicationException commEx)
            {
                Console.WriteLine($"[ERROR] Comunicación fallida al enviar el reporte: {commEx.Message}");
            }
            catch (TimeoutException timeoutEx)
            {
                Console.WriteLine($"[ERROR] Tiempo de espera agotado al enviar el reporte: {timeoutEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error al enviar el reporte: {ex.Message}");
            }
        }

        public void Disconnect(string nickname)
        {
            try
            {
                if (IsConnected && _reportManagerClient != null)
                {
                    _reportManagerClient.UnsubscribeFromBanNotifications(nickname);
                    CloseClientHelper.CloseClient(_reportManagerClient);
                }
            }
            catch (CommunicationException commEx)
            {
                _reportManagerClient.Abort();
                Console.WriteLine($"[ERROR] Comunicación fallida al desconectar " +
                    $"ReportSessionService: {commEx.Message}");
            }
            catch (TimeoutException timeoutEx)
            {
                _reportManagerClient.Abort();
                Console.WriteLine($"[ERROR] Tiempo de espera agotado al desconectar " +
                    $"ReportSessionService: {timeoutEx.Message}");
            }
            catch (Exception ex)
            {
                _reportManagerClient.Abort();
                Console.WriteLine($"[ERROR] Error al desconectar ReportSessionService: {ex.Message}");
            }
            finally
            {
                IsConnected = false;
            }
        }
    }
}
