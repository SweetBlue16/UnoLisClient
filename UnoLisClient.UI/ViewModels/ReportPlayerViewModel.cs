using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Report;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class ReportPlayerViewModel : BaseViewModel
    {
        private readonly IReportService _reportService;
        private readonly ISessionContext _sessionContext;
        private readonly IReportTrackerService _reportTracker;
        private readonly Page _view;

        public ObservableCollection<string> AvailablePlayers { get; } = new ObservableCollection<string>();

        private string _selectedPlayer;
        public string SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                if (SetProperty(ref _selectedPlayer, value))
                {
                    (SendReportCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _reportDescription;
        public string ReportDescription
        {
            get => _reportDescription;
            set
            {
                if (SetProperty(ref _reportDescription, value))
                {
                    (SendReportCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SendReportCommand { get; }
        public ICommand CancelCommand { get; }
        public event Action RequestClose;

        public ReportPlayerViewModel(IDialogService dialogService, IReportService reportService,
        ISessionContext sessionContext, IReportTrackerService reportTracker, List<string> players,
        INavigationService navigationService) : base(dialogService)
        {
            _reportService = reportService;
            _sessionContext = sessionContext;
            _reportTracker = reportTracker;

            LoadPlayers(players);

            SendReportCommand = new RelayCommand(async () => await ExecuteSendReport(), CanSendReport);
            CancelCommand = new RelayCommand(() => RequestClose?.Invoke());
        }

        private void LoadPlayers(List<string> players)
        {
            var reporterPlayer = _sessionContext.CurrentUserNickname;

            if (players != null)
            {
                foreach (var player in players)
                {
                    if (player != reporterPlayer)
                    {
                        AvailablePlayers.Add(player);
                    }
                }
            }

            if (AvailablePlayers.Any())
            {
                SelectedPlayer = AvailablePlayers.First();
            }
        }

        private bool CanSendReport()
        {
            return !string.IsNullOrEmpty(SelectedPlayer) &&
                   !string.IsNullOrWhiteSpace(ReportDescription);
        }

        public Task ExecuteSendReport()
        {
            return ExecuteSendReport(ReportDescription);
        }

        public async Task ExecuteSendReport(string reportDescription)
        {
            if (!CanSendReport())
            {
                return;
            }

            if (_reportTracker.HasReported(SelectedPlayer))
            {
                _dialogService.ShowWarning(string.Format(ErrorMessages.AlreadyReportedRecentlyMessageLabel, SelectedPlayer));
                return;
            }

            _dialogService.ShowLoading(_view);
            try
            {
                var reportData = new ReportData
                {
                    ReporterNickname = _sessionContext.CurrentUserNickname,
                    ReportedNickname = SelectedPlayer,
                    Description = reportDescription.Trim()
                };
                var response = await _reportService.ReportPlayerAsync(reportData);
                var message = MessageTranslator.GetMessage(response.Code);

                if (response.Success)
                {
                    string successMessage = string.Format(message, SelectedPlayer);
                    _reportTracker.AddReport(SelectedPlayer);
                    _dialogService.ShowAlert(Global.SuccessLabel, successMessage, PopUpIconType.Success);
                    RequestClose?.Invoke();
                }
                else
                {
                    _dialogService.ShowWarning(message);
                }
            }
            catch (EndpointNotFoundException enfEx)
            {
                string logMessage = $"Fallo al enviar el reporte a jugador: {enfEx.Message}";
                HandleException(MessageCode.ConnectionRejected, logMessage, enfEx);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Fallo al enviar el reporte a jugador: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Fallo al enviar el reporte a jugador: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fallo al enviar el reporte a jugador: {ex.Message}";
                HandleException(MessageCode.PlayerReportFailed, logMessage, ex);
            }
            finally
            {
                _dialogService.HideLoading();
            }
        }
    }
}
