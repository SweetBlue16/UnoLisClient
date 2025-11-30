using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.Logic.Services
{
    public class GameplayService : IGameplayService 
    {
        private static readonly Lazy<GameplayService> _instance =
            new Lazy<GameplayService>(() => new GameplayService());

        public static GameplayService Instance => _instance.Value;

        private IGameplayManager _proxy;
        private DuplexChannelFactory<IGameplayManager> _factory;
        private GameplayCallback _callback;
        private string _currentUserNickname;

        public event Action<string, Card> PlayerPlayedCard;
        public event Action<string> PlayerDrewCard;
        public event Action<string> TurnChanged;
        public event Action<List<Card>> InitialHandReceived;
        public event Action<List<Card>> CardsReceived;
        public event Action<List<ResultData>> GameEnded;

        private GameplayService()
        {
            GameplayCallback.OnCardPlayed += (nick, card) => PlayerPlayedCard?.Invoke(nick, card);
            GameplayCallback.OnCardDrawn += (nick) => PlayerDrewCard?.Invoke(nick);
            GameplayCallback.OnTurnChanged += (nick) => TurnChanged?.Invoke(nick);
            GameplayCallback.OnInitialHandReceived += (hand) => InitialHandReceived?.Invoke(hand);
            GameplayCallback.OnCardsReceived += (cards) => CardsReceived?.Invoke(cards);
            GameplayCallback.OnMatchEnded += (results) => GameEnded?.Invoke(results);
        }

        public void Initialize(string nickname)
        {
            if (_factory != null && _factory.State == CommunicationState.Opened) return;

            try
            {
                // 1. Instanciación del Callback (Puede fallar si hay error en su constructor)
                _currentUserNickname = nickname;
                _callback = new GameplayCallback();
                var context = new InstanceContext(_callback);

                // 2. Creación del Factory (Punto crítico de Configuración)
                // Lanza InvalidOperationException si no encuentra "NetTcpBinding_IGameplayManager" en App.config
                _factory = new DuplexChannelFactory<IGameplayManager>(context, "NetTcpBinding_IGameplayManager");

                _proxy = _factory.CreateChannel();

                // Nota: CreateChannel() es "perezoso" (lazy). No conecta a la red todavía.
                // La conexión real ocurre la primera vez que llamas a un método (ej. ConnectToGame).
                // Por eso aquí no atrapamos EndpointNotFoundException (eso va en ConnectToGame).
            }
            catch (InvalidOperationException configEx)
            {
                Logger.Error($"[GAME-CLIENT] WCF Configuration error. Verify App.config: {configEx.Message}", configEx);
                Cleanup();
                throw;
            }
            catch (System.Configuration.ConfigurationErrorsException configFileEx)
            {
                Logger.Error($"[GAME-CLIENT] Aoo.Config is corrupt: {configFileEx.Message}", configFileEx);
                Cleanup();
                throw;
            }
            catch (CommunicationException commEx)
            {
                Logger.Error($"[GAME-CLIENT] Error while creating WCF communication pile: {commEx.Message}", commEx);
                Cleanup();
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error($"[GAME-CLIENT] Unexpected error while initializing game", ex);
                Cleanup();
                throw;
            }
        }

        public Task ConnectToGameAsync(string lobbyCode, string nickname)
        {
            EnsureConnection();
            return Task.Run(() => _proxy.ConnectToGame(lobbyCode, nickname));
        }

        public Task PlayCardAsync(string lobbyCode, string nickname, string cardId, int? colorId)
        {
            EnsureConnection();
            var data = new PlayCardData
            {
                LobbyCode = lobbyCode,
                Nickname = nickname,
                CardId = cardId,
                SelectedColorId = colorId
            };

            return Task.Run(() => _proxy.PlayCard(data));
        }

        public Task DrawCardAsync(string lobbyCode, string nickname)
        {
            EnsureConnection();
            return Task.Run(() => _proxy.DrawCard(lobbyCode, nickname));
        }

        private void EnsureConnection()
        {
            if (_proxy == null || (_proxy as ICommunicationObject).State != CommunicationState.Opened)
            {
                if (!string.IsNullOrEmpty(_currentUserNickname))
                {
                    Initialize(_currentUserNickname);
                }
                else
                {
                    throw new InvalidOperationException("GameplayService not initialized properly.");
                }
            }
        }

        public void Cleanup()
        {
            if (_factory == null) return;

            try
            {
                if (_factory.State == CommunicationState.Opened)
                {
                    _factory.Close();
                }
                else
                {
                    _factory.Abort();
                }
            }
            catch (TimeoutException timeoutEx)
            {
                Logger.Warn($"[CLIENT] Timeout closing channel: {timeoutEx.Message}");
                _factory.Abort(); // Forzamos el cierre local
            }
            catch (CommunicationException commEx)
            {
                Logger.Warn($"[CLIENT] Communication error closing channel: {commEx.Message}");
                _factory.Abort();
            }
            catch (Exception ex)
            {
                Logger.Error($"[CLIENT] Unexpected error during cleanup", ex);
                _factory.Abort();
            }
            finally
            {
                _proxy = null;
                _factory = null;
                _currentUserNickname = null;
            }
        }
    }
}