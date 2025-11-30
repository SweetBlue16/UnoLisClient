using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.Logic.Services
{
    public interface IGameplayService
    {
        event Action<string, Card> PlayerPlayedCard;
        event Action<string> PlayerDrewCard;
        event Action<string> TurnChanged;
        event Action<List<Card>> InitialHandReceived;
        event Action<List<Card>> CardsReceived;
        event Action<List<ResultData>> GameEnded;

        void Initialize(string nickname);
        Task ConnectToGameAsync(string lobbyCode, string nickname);
        Task PlayCardAsync(string lobbyCode, string nickname, string cardId, int? colorId);
        Task DrawCardAsync(string lobbyCode, string nickname);
    }
}