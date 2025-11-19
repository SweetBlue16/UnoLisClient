using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows.Input;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;
using UnoLisClient.Logic.Helpers;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchLobbyViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IFriendsService _friendsService;
        private readonly ILobbyService _lobbyService;
        private readonly string _currentUserNickname;
        private readonly string _currentLobbyCode;

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

        public MatchLobbyViewModel(INavigationService navigationService, IDialogService dialogService,
                                 IFriendsService friendsService, IChatService chatService, string lobbyCode)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _friendsService = friendsService;
            _lobbyService = LobbyService.Instance;

            _currentUserNickname = CurrentSession.CurrentUserNickname;
            _currentLobbyCode = lobbyCode;

            IsChatVisible = false;
            IsSettingsVisible = false;

            LobbyCodeDisplay = $"Lobby Code: {_currentLobbyCode}";

            ChatVM = new ChatViewModel(chatService, dialogService, _currentLobbyCode, _currentUserNickname);

            InviteFriendCommand = new RelayCommand<LobbyFriendViewModel>(ExecuteInviteFriend);
            SendInvitesCommand = new RelayCommand(ExecuteSendInvites, () => !IsLoading);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            ToggleChatCommand = new RelayCommand(ExecuteToggleChat);
            LeaveMatchCommand = new RelayCommand(ExecuteLeaveMatch);
            ReadyCommand = new RelayCommand(ExecuteReady);

            InitializeLobbySlots();
        }

        private void InitializeLobbySlots()
        {
            PlayersInLobby.Clear();
            for (int i = 0; i < 4; i++) PlayersInLobby.Add(new LobbyPlayerViewModel());
        }


        public async Task OnPageLoaded()
        {
            _lobbyService.OnPlayerListUpdated += HandlePlayerListUpdated;
            _lobbyService.OnPlayerJoined += HandlePlayerJoined;
            _lobbyService.OnPlayerLeft += HandlePlayerLeft;

            await ChatVM.InitializeAsync();
            await LoadFriendsAsync();

            try
            {
                await _lobbyService.ConnectToLobbyAsync(_currentLobbyCode, _currentUserNickname);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ConnectionFailed, "No se pudo conectar al lobby.", ex);
                _navigationService.GoBack();
            }
        }

        public async Task OnPageUnloaded()
        {
            try
            {
                await _lobbyService.DisconnectFromLobbyAsync(_currentLobbyCode, _currentUserNickname);
            }
            catch 
            {
                LogManager.Error("Error while trying to disconnect.");
            }

            _lobbyService.OnPlayerListUpdated -= HandlePlayerListUpdated;
            _lobbyService.OnPlayerJoined -= HandlePlayerJoined;
            _lobbyService.OnPlayerLeft -= HandlePlayerLeft;

            await ChatVM.CleanupAsync();
        }

        private void HandlePlayerListUpdated(string[] playerNicknames)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                ReadyStatusText = $"{playerNicknames.Length} / 4 Players";

                for (int i = 0; i < 4; i++)
                {
                    if (i < playerNicknames.Length)
                    {
                        string nick = playerNicknames[i];
                        // TODO: Cuando el servidor mande avatares, usaremos el real.
                        // Por ahora usamos uno random o fijo para probar.
                        string avatar = "/Avatars/Gatito.png";

                        PlayersInLobby[i].FillSlot(nick, avatar);
                    }
                    else
                    {
                        PlayersInLobby[i].ClearSlot();
                    }
                }
            });
        }

        private void HandlePlayerJoined(string nickname)
        {
            SoundManager.PlaySuccess();
        }

        private void HandlePlayerLeft(string nickname)
        {
            LogManager.Info($"Player left: {nickname}");
        }

        private async Task LoadFriendsAsync()
        {
            IsLoading = true;
            try
            {
                var friendList = await _friendsService.GetFriendsListAsync(_currentUserNickname);

                Friends.Clear();
                foreach (var friend in friendList.OrderBy(f => f.Nickname))
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

        private void ExecuteSendInvites()
        {
            SoundManager.PlayClick();
            var invited = Friends.Where(f => f.Invited).Select(f => f.Nickname).ToList();

            if (invited.Any())
            {
                _dialogService.ShowAlert("UNO LIS", $"Invitations sent to: {string.Join(", ", invited)}");
                // TODO: Aquí irá tu lógica para enviar los emails con el _lobbyChannelId
            }
            else
            {
                _dialogService.ShowWarning("No friends selected for invitation.");
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
            //TODO: Llamar al servicio para salir del lobby en el servidor
            _navigationService.NavigateTo(new MainMenuPage());
        }

        private void ExecuteReady()
        {
            SoundManager.PlayClick();
            // TODO: Aquí irá la lógica para avisar al servidor que estás listo

            _navigationService.NavigateTo(new MatchBoardPage());
        }
    }
}
