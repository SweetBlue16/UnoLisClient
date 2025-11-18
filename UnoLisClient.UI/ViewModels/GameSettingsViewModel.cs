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
    public class GameSettingsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMatchmakingService _matchmakingService;

        private int _playerCount = 2;
        public int PlayerCount
        {
            get => _playerCount;
            set => SetProperty(ref _playerCount, value);
        }

        private bool _isSpecialRulesEnabled;
        public bool IsSpecialRulesEnabled
        {
            get => _isSpecialRulesEnabled;
            set => SetProperty(ref _isSpecialRulesEnabled, value);
        }

        private bool _isMusicEnabled = true;
        public bool IsMusicEnabled
        {
            get => _isMusicEnabled;
            set
            {
                SetProperty(ref _isMusicEnabled, value);
                // TODO: Conectar con SoundManager
            }
        }

        private bool _isSfxEnabled = true;
        public bool IsSfxEnabled
        {
            get => _isSfxEnabled;
            set
            {
                SetProperty(ref _isSfxEnabled, value);
                // TODO: Conectar con SoundManager
            }
        }

        public ICommand CreateMatchCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand SetPlayerCountCommand { get; }

        public GameSettingsViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IMatchmakingService matchmakingService)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _matchmakingService = matchmakingService;

            CreateMatchCommand = new RelayCommand(async () => await ExecuteCreateMatchAsync(), () => !IsLoading);
            CloseCommand = new RelayCommand(ExecuteClose);
            SetPlayerCountCommand = new RelayCommand<int>(ExecuteSetPlayerCount);
        }

        private void ExecuteSetPlayerCount(int count)
        {
            PlayerCount = count;
        }

        private void ExecuteClose()
        {
            _navigationService.GoBack();
        }

        private async Task ExecuteCreateMatchAsync()
        {
            IsLoading = true;
            try
            {
                var settings = new MatchSettings
                {
                    HostNickname = CurrentSession.CurrentUserNickname,
                    MaxPlayers = this.PlayerCount,
                    UseSpecialRules = this.IsSpecialRulesEnabled
                };

                CreateMatchResponse response = await _matchmakingService.CreateMatchAsync(settings);

                if (response.Success)
                {
                    _dialogService.ShowAlert("Partida Creada", $"¡Éxito! Código de sala: {response.LobbyCode}");

                    // TODO (Fase 4): Pasar el 'response.LobbyCode' al constructor de MatchLobbyPage
                    // TODO (Futuro): Cambiar esto para navegar primero a 'SelectBackgroundPage'
                    _navigationService.NavigateTo(new MatchLobbyPage(response.LobbyCode));
                }
                else
                {
                    HandleException(MessageCode.UnhandledException, response.Message, null);
                }
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.UnhandledException, "Error al intentar crear la partida", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
