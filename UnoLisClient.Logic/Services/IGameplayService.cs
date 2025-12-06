using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.Logic.Services
{
    public interface IGameplayService
    {
        event Action<string, Card, int> PlayerPlayedCard;
        event Action<string, int> PlayerDrewCard;
        event Action<string> TurnChanged;
        event Action<List<Card>> InitialHandReceived;
        event Action<List<GamePlayer>> PlayerListReceived;
        event Action<List<Card>> CardsReceived;
        event Action<List<ResultData>> GameEnded;
        event Action<string> GameMessageReceived;
        event Action<string> PlayerShoutedUnoReceived;

        void Initialize(string nickname);
        Task ConnectToGameAsync(string lobbyCode, string nickname);
        Task PlayCardAsync(string lobbyCode, string nickname, string cardId, int? colorId);
        Task DrawCardAsync(string lobbyCode, string nickname);
        Task SayUnoAsync(string lobbyCode, string nickname);
    }
}