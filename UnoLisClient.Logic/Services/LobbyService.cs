using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.LobbyDuplex;

namespace UnoLisClient.Logic.Services
{
    /// <summary>
    /// Singleton service for managing lobby interactions via WCF duplex communication.
    /// </summary>
    public class LobbyService : ILobbyService
    {
        private static readonly Lazy<LobbyService> _instance =
            new Lazy<LobbyService>(() => new LobbyService());

        public static ILobbyService Instance => _instance.Value;

        private LobbyDuplexManagerClient _proxy;
        private readonly LobbyCallback _callback;

        public event Action<string> OnPlayerJoined;
        public event Action<string> OnPlayerLeft;
        public event Action<LobbyPlayerData[]> OnPlayerListUpdated;
        public event Action<string, bool> OnPlayerReadyStatusChanged;
        public event Action OnGameStarted;

        private LobbyService()
        {
            _callback = new LobbyCallback();

            _callback.PlayerJoinedReceived += (nick) => OnPlayerJoined?.Invoke(nick);
            _callback.PlayerLeftReceived += (nick) => OnPlayerLeft?.Invoke(nick);
            _callback.PlayerListUpdatedReceived += (list) => OnPlayerListUpdated?.Invoke(list);
            _callback.PlayerReadyStatusReceived += (nick, status) => OnPlayerReadyStatusChanged?.Invoke(nick, status);
            _callback.GameStartedReceived += () => OnGameStarted?.Invoke();
        }

        public async Task ConnectToLobbyAsync(string lobbyCode, string nickname)
        {
            InstanceContext _context;
            try
            {
                if (_proxy != null && _proxy.State == CommunicationState.Opened)
                {
                    await _proxy.ConnectToLobbyAsync(lobbyCode, nickname);
                    return;
                }

                _context = new InstanceContext(_callback);
                _proxy = new LobbyDuplexManagerClient(_context, "NetTcpBinding_ILobbyDuplexManager");
                ServerConnectionMonitor.Monitor(_proxy.InnerChannel);

                await _proxy.ConnectToLobbyAsync(lobbyCode, nickname);
            }
            catch (EndpointNotFoundException enfe)
            {
                Logger.Error($"Lobby server endpoint not found for lobby {lobbyCode}: {enfe.Message}", enfe);
                AbortProxy();
                throw;
            }
            catch (TimeoutException tex)
            {
                Logger.Error($"Timeout connecting to lobby {lobbyCode}: {tex.Message}", tex);
                AbortProxy();
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error connecting to lobby {lobbyCode}: {ex.Message}", ex);
                AbortProxy();
                throw;
            }
        }

        public async Task DisconnectFromLobbyAsync(string lobbyCode, string nickname)
        {
            if (_proxy == null) return;

            try
            {
                await _proxy.DisconnectFromLobbyAsync(lobbyCode, nickname);
                _proxy.Close(); 
            }
            catch (Exception ex)
            {
                Logger.Error($"Error disconnecting from lobby: {ex.Message}", ex);
                AbortProxy();
            }
        }

        public async Task SetReadyStatusAsync(string lobbyCode, string nickname, bool isReady)
        {
            if (_proxy == null) return;
            try
            {
                await _proxy.SetReadyStatusAsync(lobbyCode, nickname, isReady);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error setting ready status: {ex.Message}", ex);
            }
        }

        private void AbortProxy()
        {
            if (_proxy != null)
            {
                try 
                { 
                    _proxy.Abort(); 
                } 
                catch 
                {
                    Logger.Error("Error aborting the lobby proxy.");
                }
                _proxy = null;
            }
        }

    }
}
