using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Matchmaking;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class JoinMatchViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMatchmakingService _matchmakingService;

        private string _lobbyCode;
        public string LobbyCode
        {
            get => _lobbyCode;
            set
            {
                // Convertir a mayúsculas automáticamente para mejor UX
                SetProperty(ref _lobbyCode, value?.ToUpper());
            }
        }

        public ICommand JoinMatchCommand { get; }
        public ICommand CancelCommand { get; }

        public JoinMatchViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IMatchmakingService matchmakingService)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _matchmakingService = matchmakingService;

            JoinMatchCommand = new RelayCommand(async () => await ExecuteJoinMatchAsync(), CanJoinMatch);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanJoinMatch()
        {
            return !IsLoading && !string.IsNullOrWhiteSpace(LobbyCode) && LobbyCode.Length == 5;
        }

        private async Task ExecuteJoinMatchAsync()
        {
            IsLoading = true;
            try
            {
                string nickname = CurrentSession.CurrentUserNickname;

                JoinMatchResponse response = await _matchmakingService.JoinMatchAsync(this.LobbyCode, nickname);

                if (response.Success)
                {
                    // TODO (Fase 4): Pasar el 'response.LobbyCode' al constructor
                    _navigationService.NavigateTo(new MatchLobbyPage(response.LobbyCode));
                }
                else
                {
                    _dialogService.ShowWarning(response.Message);
                }
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.UnhandledException, "Error joining match", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteCancel()
        {
            _navigationService.NavigateTo(new MatchMenuPage());
        }
    }
}
