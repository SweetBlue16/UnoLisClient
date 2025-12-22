using System;
using System.Collections.Generic;
using System.Linq;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.Logic.Callbacks
{
    public class GameplayCallback : IGameplayManagerCallback
    {
        public static event Action<string, Card, int> OnCardPlayed;
        public static event Action<string, int> OnCardDrawn;
        public static event Action<string> OnTurnChanged;
        public static event Action<List<ResultData>> OnMatchEnded;
        public static event Action<List<Card>> OnInitialHandReceived;
        public static event Action<List<Card>> OnCardsReceived;
        public static event Action<List<GamePlayer>> OnPlayerListReceived;
        public static event Action<string> OnGameMessageReceived;
        public static event Action<string> OnPlayerShoutedUno;
        public static event Action<string, ItemType> OnPlayerUsedItem;

        public void CardPlayed(string nickname, Card card, int cardsLeft)
        {
            OnCardPlayed?.Invoke(nickname, card, cardsLeft);
        }

        public void CardDrawn(string nickname, int cardsLeft)
        {
            OnCardDrawn?.Invoke(nickname, cardsLeft);
        }

        public void TurnChanged(string nextPlayerNickname)
        {
            OnTurnChanged?.Invoke(nextPlayerNickname);
        }

        public void MatchEnded(ResultData[] results)
        {
            OnMatchEnded?.Invoke(results.ToList());
        }

        public void ReceiveInitialHand(Card[] hand)
        {
            OnInitialHandReceived?.Invoke(hand.ToList());
        }

        public void ReceiveCards(Card[] cards)
        {
            OnCardsReceived?.Invoke(cards.ToList());
        }

        public void ReceivePlayerList(GamePlayer[] players)
        {
            OnPlayerListReceived?.Invoke(players.ToList());
        }

        public void GameMessage(string message)
        {
            OnGameMessageReceived?.Invoke(message);
        }

        public void PlayerShoutedUno(string nickname)
        {
            OnPlayerShoutedUno?.Invoke(nickname);
        }

        public void PlayerUsedItem(string nickname, ItemType itemType)
        {
            OnPlayerUsedItem?.Invoke(nickname, itemType);
        }
    }
}