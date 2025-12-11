using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.LobbyDuplex;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.UI.Views.UnoLisWindows;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchLobbyViewModel : BaseViewModel
    {
        private const string DefaultAvatar = "LogoUNO";
        private const string AvatarsBasePathFormat = "pack://application:,,,/Avatars/";
        private const string AvatarFileExtension = ".png";

        private readonly INavigationService _navigationService;
        private readonly IFriendsService _friendsService;
        private readonly ILobbyService _lobbyService;
        private readonly IMatchmakingService _matchmakingService;

        private readonly string _currentUserNickname;
        private readonly string _currentLobbyCode;

        private bool _isMeReady = false;
        private bool _isNavigatingToGame = false;

        public ChatViewModel ChatVM { get; }

        private bool _isChatVisible;
        public bool IsChatVisible
        {
            get => _isChatVisible;
            set => SetProperty(ref _isChatVisible, value);
        }

        private bool _isSettingsVisible;
        public bool IsSettingsVisible
        {
            get => _isSettingsVisible;
            set => SetProperty(ref _isSettingsVisible, value);
        }

        private string _lobbyCodeDisplay;
        public string LobbyCodeDisplay
        {
            get => _lobbyCodeDisplay;
            set => SetProperty(ref _lobbyCodeDisplay, value);
        }

        private string _readyStatusText = "Esperando jugadores...";
        public string ReadyStatusText
        {
            get => _readyStatusText;
            set => SetProperty(ref _readyStatusText, value);
        }

        public ObservableCollection<LobbyFriendViewModel> Friends { get; } = new ObservableCollection<LobbyFriendViewModel>();
        public ObservableCollection<LobbyPlayerViewModel> PlayersInLobby { get; } = new ObservableCollection<LobbyPlayerViewModel>();

        public ICommand InviteFriendCommand { get; }
        public ICommand SendInvitesCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand ToggleChatCommand { get; }
        public ICommand LeaveMatchCommand { get; }
        public ICommand ReadyCommand { get; }
        public ICommand OpenReportWindowCommand { get; }

        public MatchLobbyViewModel(INavigationService navigationService, IDialogService dialogService,
                                 IFriendsService friendsService, IChatService chatService, string lobbyCode)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _friendsService = friendsService;

            _lobbyService = LobbyService.Instance;
            _matchmakingService = MatchmakingService.Instance;

            _currentUserNickname = CurrentSession.CurrentUserNickname;
            _currentLobbyCode = lobbyCode;

            IsChatVisible = false;
            IsSettingsVisible = false;
            LobbyCodeDisplay = string.Format(Lobby.LobbyCodeLabel, _currentLobbyCode);

            ChatVM = new ChatViewModel(chatService, dialogService, _currentLobbyCode, _currentUserNickname);

            InviteFriendCommand = new RelayCommand<LobbyFriendViewModel>(ExecuteInviteFriend);
            SendInvitesCommand = new RelayCommand(ExecuteSendInvites, () => !IsLoading);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            ToggleChatCommand = new RelayCommand(ExecuteToggleChat);
            LeaveMatchCommand = new RelayCommand(ExecuteLeaveMatch);
            ReadyCommand = new RelayCommand(ExecuteReady);
            OpenReportWindowCommand = new RelayCommand(ExecuteOpenReportWindow);

            InitializeLobbySlots();
        }

        private void ExecuteOpenReportWindow()
        {
            SoundManager.PlayClick();
            var playersList = PlayersInLobby
                .Where(p => p.IsSlotFilled)
                .Select(p => p.Nickname)
                .ToList();
            IsSettingsVisible = false;
            var reportWindow = new ReportPlayerWindow(playersList);
            reportWindow.Owner = Window.GetWindow(_navigationService as Page);
            reportWindow.ShowDialog();
        }

        private void InitializeLobbySlots()
        {
            int lobbyPlayers = 4;
            PlayersInLobby.Clear();
            for (int i = 0; i < lobbyPlayers; i++)
            {
                PlayersInLobby.Add(new LobbyPlayerViewModel());
            }
        }

        public async Task OnPageLoaded()
        {
            _lobbyService.OnPlayerListUpdated += HandlePlayerListUpdated;
            _lobbyService.OnPlayerJoined += HandlePlayerJoined;
            _lobbyService.OnPlayerLeft += HandlePlayerLeft;

            _lobbyService.OnPlayerReadyStatusChanged += HandlePlayerReadyStatus;
            _lobbyService.OnGameStarted += HandleGameStarted;

            try
            {
                await _lobbyService.ConnectToLobbyAsync(_currentLobbyCode, _currentUserNickname);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ConnectionFailed, "Could not connect to Lobby.", ex);
                _navigationService.GoBack();
                return;
            }

            try
            {
                await ChatVM.InitializeAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to initialize chat: {ex.Message}");
            }

            if (!UserHelper.IsGuest(_currentUserNickname))
            {
                await LoadFriendsAsync();
            }
            else
            {
                Friends.Clear();
            }
        }

        public async Task OnPageUnloaded()
        {
            if (_isNavigatingToGame)
            {
                return;
            }

            try
            {
                await _lobbyService.DisconnectFromLobbyAsync(_currentLobbyCode, _currentUserNickname);
            }
            catch 
            {
                Logger.Error("Error while trying to disconnect.");
            }

            _lobbyService.OnPlayerListUpdated -= HandlePlayerListUpdated;
            _lobbyService.OnPlayerJoined -= HandlePlayerJoined;
            _lobbyService.OnPlayerLeft -= HandlePlayerLeft;

            _lobbyService.OnPlayerReadyStatusChanged -= HandlePlayerReadyStatus;
            _lobbyService.OnGameStarted -= HandleGameStarted;

            await ChatVM.CleanupAsync();
        }

        private void HandlePlayerListUpdated(LobbyPlayerData[] players)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                int lobbyPlayers = 4;
                for (int i = 0; i < lobbyPlayers; i++)
                {
                    if (i < players.Length)
                    {
                        var player = players[i];
                        string nick = player.Nickname;
                        string avatarName = string.IsNullOrEmpty(player.AvatarName) ? DefaultAvatar : player.AvatarName;
                        string avatarPath = $"{AvatarsBasePathFormat}{avatarName}{AvatarFileExtension}";

                        PlayersInLobby[i].FillSlot(nick, avatarPath);
                        PlayersInLobby[i].IsReady = player.IsReady;
                    }
                    else
                    {
                        PlayersInLobby[i].ClearSlot();
                    }
                }

                UpdateReadyCount();
            });
        }

        private void HandlePlayerReadyStatus(string nickname, bool isReady)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var playerSlot = PlayersInLobby.FirstOrDefault(p => p.Nickname == nickname);
                if (playerSlot != null)
                {
                    playerSlot.IsReady = isReady; 
                    SoundManager.PlayClick();
                }

                UpdateReadyCount();
            });
        }

        private void UpdateReadyCount()
        {
            int totalPlayers = PlayersInLobby.Count(player => player.IsSlotFilled);
            int readyCount = PlayersInLobby.Count(player => player.IsSlotFilled && player.IsReady);
            ReadyStatusText = $"{readyCount} / {totalPlayers} {Lobby.PlayerReadyLabel}";
        }

        private void HandleGameStarted()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _isNavigatingToGame = true;
                _navigationService.NavigateTo(new MatchBoardPage(_currentLobbyCode));
            });
        }

        private void HandlePlayerJoined(string nickname)
        {
            SoundManager.PlaySuccess();
        }

        private void HandlePlayerLeft(string nickname)
        {
            Logger.Info($"Player left: {nickname}");
        }

        private async void ExecuteReady()
        {
            _isMeReady = !_isMeReady;
            try
            {
                await _lobbyService.SetReadyStatusAsync(_currentLobbyCode, _currentUserNickname, _isMeReady);
            }
            catch (Exception ex)
            {
                _isMeReady = !_isMeReady;
                HandleException(MessageCode.ConfirmationInternalError, "Error setting ready status", ex);
            }
        }

        private async Task LoadFriendsAsync()
        {

            if (UserHelper.IsGuest(_currentUserNickname))
            {
                return;
            }

            IsLoading = true;
            try
            {
                var friendList = await _friendsService.GetFriendsListAsync(_currentUserNickname);

                Friends.Clear();
                foreach (var friend in friendList.OrderBy(friend => friend.Nickname))
                {
                    var friendViewModel = new LobbyFriendViewModel(friend);
                    Friends.Add(friendViewModel);
                }
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.FriendsInternalError, $"Error al cargar la lista de amigos: {ex.Message}", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteInviteFriend(LobbyFriendViewModel friend)
        {
            if (friend == null) return;
            SoundManager.PlayClick();
            friend.Invited = !friend.Invited;
        }

        private async void ExecuteSendInvites()
        {
            SoundManager.PlayClick();
            var invitedNicknames = Friends.Where(friend => friend.Invited).Select(friend => friend.Nickname).ToList();

            if (!invitedNicknames.Any())
            {
                _dialogService.ShowWarning("No friends selected for invitation.");
                return;
            }

            IsLoading = true;
            try
            {
                bool success = await _matchmakingService.SendInvitationsAsync(
                    _currentLobbyCode,
                    _currentUserNickname,
                    invitedNicknames);

                if (success)
                {
                    _dialogService.ShowAlert(Lobby.InvitationsSentLabel, Lobby.InvitationsSentMessageLabel, PopUpIconType.Success);
                    foreach (var friend in Friends)
                    {
                        friend.Invited = false;
                    }
                }
                else
                {
                    _dialogService.ShowWarning(ErrorMessages.CouldNotSentInvitationsMessageLabel);
                }
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ConfirmationInternalError, "Error sending invites", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteToggleSettings()
        {
            SoundManager.PlayClick();
            IsSettingsVisible = !IsSettingsVisible;
        }

        private void ExecuteToggleChat()
        {
            SoundManager.PlayClick();
            IsChatVisible = !IsChatVisible;
        }

        private void ExecuteLeaveMatch()
        {
            _navigationService.NavigateTo(new MainMenuPage());
        }
    }
}
