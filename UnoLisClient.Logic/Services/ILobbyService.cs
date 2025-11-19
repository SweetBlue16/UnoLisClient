using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoLisClient.Logic.Services
{
    public interface ILobbyService
    {
        event Action<string> OnPlayerJoined;
        event Action<string> OnPlayerLeft;
        event Action<string[]> OnPlayerListUpdated;
        event Action<string, bool> OnPlayerReadyStatusChanged;

        Task ConnectToLobbyAsync(string lobbyCode, string nickname);
        Task DisconnectFromLobbyAsync(string lobbyCode, string nickname);
        Task SetReadyStatusAsync(string lobbyCode, string nickname, bool isReady);
    }
}
