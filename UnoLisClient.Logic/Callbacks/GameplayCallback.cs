using System;
using System.Collections.Generic;
using System.Linq;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.Logic.Callbacks
{
    public class GameplayCallback : IGameplayManagerCallback
    {
        public static event Action<string, Card> OnCardPlayed;
        public static event Action<string> OnCardDrawn;
        public static event Action<string> OnTurnChanged;
        public static event Action<List<ResultData>> OnMatchEnded;
        public static event Action<List<Card>> OnInitialHandReceived;
        public static event Action<List<Card>> OnCardsReceived;
        public static event Action<List<string>> OnPlayerListReceived;

        public void CardPlayed(string nickname, Card card)
        {
            OnCardPlayed?.Invoke(nickname, card);
        }

        public void CardDrawn(string nickname)
        {
            OnCardDrawn?.Invoke(nickname);
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

        public void ReceivePlayerList(string[] players)
        {
            OnPlayerListReceived?.Invoke(players.ToList());
        }
    }
}