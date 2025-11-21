using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Matchmaking;

namespace UnoLisClient.Logic.Services
{
    /// <summary>
    /// Attempts to create a new match on the server.
    /// </summary>
    public interface IMatchmakingService
    {
        Task<CreateMatchResponse> CreateMatchAsync(MatchSettings settings);
        Task<JoinMatchResponse> JoinMatchAsync(string lobbyCode, string nickname);
        Task<bool> SendInvitationsAsync(string lobbyCode, string senderNickname, List<string> invitedNicknames);

        Task<bool> SetLobbyBackgroundAsync(string lobbyCode, string backgroundName);

        Task<LobbySettings> GetLobbySettingsAsync(string lobbyCode);
    }
}
