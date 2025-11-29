using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.LobbyDuplex;
using UnoLisClient.Logic.UnoLisServerReference.Login;

namespace UnoLisClient.Logic.Callbacks
{
    /// <summary>
    /// Receives callbacks from the lobby duplex WCF service and raises corresponding events.
    /// </summary>
    public class LobbyCallback : ILobbyDuplexManagerCallback
    {
        public event Action<string> PlayerJoinedReceived;
        public event Action<string> PlayerLeftReceived;
        public event Action<LobbyPlayerData[]> PlayerListUpdatedReceived;
        public event Action<string, bool> PlayerReadyStatusReceived;
        public event Action GameStartedReceived;

        public void PlayerJoined(string nickname, string avatarName)
        {
            PlayerJoinedReceived?.Invoke(nickname);
        }

        public void PlayerLeft(string nickname)
        {
            PlayerLeftReceived?.Invoke(nickname);
        }

        public void UpdatePlayerList(LobbyPlayerData[] nicknames)
        {
            PlayerListUpdatedReceived?.Invoke(nicknames);
        }

        public void PlayerReadyStatusChanged(string nickname, bool isReady)
        {
            PlayerReadyStatusReceived?.Invoke(nickname, isReady);
        }

        public void GameStarted()
        {
            GameStartedReceived?.Invoke();
        }
    }
}
