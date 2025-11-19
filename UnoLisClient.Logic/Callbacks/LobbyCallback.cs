using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.LobbyDuplex;

namespace UnoLisClient.Logic.Callbacks
{
    /// <summary>
    /// Recibe notificaciones en tiempo real del Lobby desde el servidor.
    /// </summary>
    public class LobbyCallback : ILobbyDuplexManagerCallback
    {
        public event Action<string> PlayerJoinedReceived;
        public event Action<string> PlayerLeftReceived;
        public event Action<string[]> PlayerListUpdatedReceived;
        public event Action<string, bool> PlayerReadyStatusReceived;

        public void PlayerJoined(string nickname)
        {
            PlayerJoinedReceived?.Invoke(nickname);
        }

        public void PlayerLeft(string nickname)
        {
            PlayerLeftReceived?.Invoke(nickname);
        }

        public void UpdatePlayerList(string[] nicknames)
        {
            PlayerListUpdatedReceived?.Invoke(nicknames);
        }

        public void PlayerReadyStatusChanged(string nickname, bool isReady)
        {
            PlayerReadyStatusReceived?.Invoke(nickname, isReady);
        }
    }
}
