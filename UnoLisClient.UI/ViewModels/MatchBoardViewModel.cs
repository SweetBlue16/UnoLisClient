using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchBoardViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMatchmakingService _matchmakingService;
        private readonly IGameplayService _gameplayService;
        private readonly Page _view;

        public event Action<string> RequestSetBackground;

        private string _currentLobbyCode;
        private string _currentUserNickname;
        private List<string> _allPlayersOrdered;

        private CardModel _discardPileTopCard;
        public CardModel DiscardPileTopCard 
        { 
            get => _discardPileTopCard; 
            set => SetProperty(ref _discardPileTopCard, value);
        }

        private CardModel _deckPileCard;
        public CardModel DeckPileCard
        {
            get => _deckPileCard;
            set => SetProperty(ref _deckPileCard, value);
        }

        private string _currentTurnNickname = "...";
        public string CurrentTurnNickname
        {
            get => _currentTurnNickname;
            set => SetProperty(ref _currentTurnNickname, value);
        }

        public ObservableCollection<CardModel> PlayerHand { get; }
            = new ObservableCollection<CardModel>();
        public OpponentModel OpponentTop { get; set; }
        public OpponentModel OpponentLeft { get; set; }
        public OpponentModel OpponentRight { get; set; }

        public ObservableCollection<ItemModel> Items { get; }
            = new ObservableCollection<ItemModel>();

        private bool _isUnoButtonVisible;
        public bool IsUnoButtonVisible 
        {
            get => _isUnoButtonVisible;
            set => SetProperty(ref _isUnoButtonVisible, value);
        }

        private bool _isSettingsMenuVisible;
        public bool IsSettingsMenuVisible
        {
            get => _isSettingsMenuVisible;
            set => SetProperty(ref _isSettingsMenuVisible, value);
        }

        public ICommand DrawCardCommand { get; }
        public ICommand CallUnoCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand LeaveMatchCommand { get; }
        public ICommand ReportPlayerCommand { get; }

        public MatchBoardViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _matchmakingService = MatchmakingService.Instance;
            _gameplayService = GameplayService.Instance;

            _currentUserNickname = Logic.Models.CurrentSession.CurrentUserNickname;
            _gameplayService.Initialize(_currentUserNickname);

            DrawCardCommand = new RelayCommand(ExecuteDrawCard);
            CallUnoCommand = new RelayCommand(ExecuteCallUno);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            LeaveMatchCommand = new RelayCommand(ExecuteExitGame);
            ReportPlayerCommand = new RelayCommand(ExecuteReportPlayer);

            PlayerHand = new ObservableCollection<CardModel>();
        }

        public async Task InitializeMatchAsync(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode)) return;

            _currentLobbyCode = lobbyCode;

            // LOG: Confirmamos que entró al método
            System.Diagnostics.Debug.WriteLine($"[CLIENT] Initializing match for: {lobbyCode}");

            // 1. Delay Táctico (Vital para WCF Duplex)
            await Task.Delay(1000);

            try
            {
                // Suscripciones
                _gameplayService.InitialHandReceived += HandleInitialHand;
                _gameplayService.CardsReceived += HandleCardsReceived;
                _gameplayService.PlayerPlayedCard += HandlePlayerPlayedCard;
                _gameplayService.PlayerDrewCard += HandlePlayerDrewCard;
                _gameplayService.TurnChanged += HandleTurnChanged;

                // Carga de Configuración (Fondo)
                try
                {
                    var settings = await _matchmakingService.GetLobbySettingsAsync(lobbyCode);
                    if (settings != null && !string.IsNullOrEmpty(settings.BackgroundVideoName))
                    {
                        RequestSetBackground?.Invoke(settings.BackgroundVideoName);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] Background error (ignored): {ex.Message}");
                }

                // Conexión con Reintentos
                int retries = 3;
                while (retries > 0)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"[CLIENT] Connecting... (Attempt {4 - retries}/3)");

                        await _gameplayService.ConnectToGameAsync(lobbyCode, _currentUserNickname);

                        System.Diagnostics.Debug.WriteLine("[CLIENT] Connect command sent successfully.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        retries--;
                        System.Diagnostics.Debug.WriteLine($"[CLIENT] Connection failed: {ex.Message}. Retries left: {retries}");
                        if (retries == 0) throw;
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                // Error Fatal
                System.Diagnostics.Debug.WriteLine($"[CLIENT] FATAL INIT ERROR: {ex.Message}");
                // Logger.Error($"FATAL Error: {ex.Message}"); // Si tienes logger

                // NO hacemos GoBack() para poder leer los logs sin que se cierre la página
                _dialogService.ShowAlert("Game Error", $"Could not connect: {ex.Message}", PopUpIconType.Error);
            }
        }

        private void HandleInitialHand(List<Card> hand)
        {
            // LOG CRÍTICO: ¿Llegó el mensaje?
            System.Diagnostics.Debug.WriteLine($"[CLIENT] EVENT RECEIVED: Initial Hand with {hand?.Count ?? 0} cards.");

            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    PlayerHand.Clear();
                    if (hand != null)
                    {
                        foreach (var cardDto in hand)
                        {
                            // LOG DETALLADO: ¿Qué carta es?
                            System.Diagnostics.Debug.WriteLine($"[CLIENT] Adding visual card: {cardDto.Color} {cardDto.Value} (ID: {cardDto.Id})");
                            AddCardToHand(cardDto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si falla aquí, es un error de UI (Assets no encontrados, etc.)
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] UI ERROR Rendering Cards: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            });
        }

        private void HandleCardsReceived(List<Card> cards)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var cardDto in cards)
                {
                    AddCardToHand(cardDto);
                }
                UpdateUnoButtonStatus();
                SoundManager.PlaySound("shuffle_deck.mp3"); // Sonido opcional
            });
        }

        private void HandlePlayerPlayedCard(string nickname, Card card)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string imagePath = CardAssetHelper.GetImagePath(card.Color, card.Value);
                var displayCard = new Card { Color = card.Color, Value = card.Value, Id = card.Id };
                var playedCardModel = new CardModel(displayCard, null);

                playedCardModel.ImagePath = imagePath;

                DiscardPileTopCard = playedCardModel;

                if (nickname == _currentUserNickname)
                {
                    var cardToRemove = PlayerHand.FirstOrDefault(c => c.CardData.Id == card.Id);
                    if (cardToRemove != null) PlayerHand.Remove(cardToRemove);
                }
                else
                {
                    UpdateOpponentCardCount(nickname, -1);
                }
                SoundManager.PlaySound("card_flip.mp3");
                UpdateUnoButtonStatus();
            });
        }

        private void HandlePlayerDrewCard(string nickname)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (nickname != _currentUserNickname)
                {
                    UpdateOpponentCardCount(nickname, 1); // Sumar 1 carta al rival
                    // Animación visual opcional aquí
                }
            });
        }

        private void HandleTurnChanged(string nickname)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentTurnNickname = nickname;
                bool isMyTurn = (nickname == _currentUserNickname);

                foreach (var card in PlayerHand)
                {
                    card.IsPlayable = true; // Falta validar color/valor (lo vemos en Fase 2)
                    card.UpdateCanExecute();
                }

                // Actualizar UI de oponentes (borde dorado, etc.)
                // UpdateOpponentTurnVisuals(nickname); 
            });
        }

        private async void ExecutePlayCard(CardModel cardModel)
        {
            if (CurrentTurnNickname != _currentUserNickname)
            {
                return;
            }

            try
            {
                int? colorId = null;
                if (IsWild(cardModel.CardData.Value))
                {
                    // TODO: Mostrar Popup de selección de color
                    // var selectedColor = _dialogService.ShowColorPicker();
                    colorId = (int)CardColor.Red; // Temporal hardcodeado
                }

                await _gameplayService.PlayCardAsync(_currentLobbyCode, _currentUserNickname, cardModel.CardData.Id, colorId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing card: {ex.Message}");
            }
        }

        private async void ExecuteDrawCard()
        {
            // Validar turno si quieres restringir robo fuera de turno
            // if (CurrentTurnNickname != _currentUserNickname) return;

            await _gameplayService.DrawCardAsync(_currentLobbyCode, _currentUserNickname);
        }

        private void AddCardToHand(Card cardDto)
        {
            string path = CardAssetHelper.GetImagePath(cardDto.Color, cardDto.Value);
            var model = new CardModel(cardDto, (c) => ExecutePlayCard(c));

            model.ImagePath = path;
            model.IsPlayable = (CurrentTurnNickname == _currentUserNickname);

            PlayerHand.Add(model);
        }

        private bool IsWild(CardValue value)
        {
            return value == CardValue.Wild || value == CardValue.WildDrawFour ||
                   value == CardValue.WildDrawTen || value == CardValue.WildDrawSkipReverseFour;
        }

        private void UpdateOpponentCardCount(string nickname, int delta)
        {
            // Buscamos en los 3 espacios quién tiene ese nick
            if (OpponentLeft?.Nickname == nickname) OpponentLeft.CardCount += delta;
            else if (OpponentTop?.Nickname == nickname) OpponentTop.CardCount += delta;
            else if (OpponentRight?.Nickname == nickname) OpponentRight.CardCount += delta;
        }

        private void ExecuteCallUno()
        {
            SoundManager.PlayClick();
            IsUnoButtonVisible = false;
            _dialogService.ShowAlert(Global.AppNameLabel, string.Format(Match.PlayerDeclaredUnoMessageLabel, CurrentTurnNickname), PopUpIconType.Info);
        }

        private void UpdateUnoButtonStatus()
        {
            IsUnoButtonVisible = (PlayerHand.Count == 2);
        }

        private void HandlePlayerListReceived(List<string> players)
        {
            _allPlayersOrdered = players;
            InitializeOpponents();
        }

        private void InitializeOpponents()
        {
            if (_allPlayersOrdered == null || !_allPlayersOrdered.Contains(_currentUserNickname)) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                int myIndex = _allPlayersOrdered.IndexOf(_currentUserNickname);
                int totalPlayers = _allPlayersOrdered.Count;

                // Limpiar oponentes visuales
                OpponentLeft = null;
                OpponentTop = null;
                OpponentRight = null;
                // (Asegúrate de notificar a la vista con OnPropertyChanged si no usas ObservableObject automático)
                // Ojo: Si OpponentModel implementa INotifyPropertyChanged, mejor actualiza sus propiedades en lugar de anular el objeto.
                // Asumiré que re-creas los objetos o limpias sus datos.

                // Algoritmo de Mesa Redonda
                for (int i = 1; i < totalPlayers; i++)
                {
                    // Calculamos el índice del rival relativo a mí (sentido horario)
                    int rivalIndex = (myIndex + i) % totalPlayers;
                    string rivalNick = _allPlayersOrdered[rivalIndex];

                    // Posicionamiento visual según cantidad de jugadores
                    if (totalPlayers == 2)
                    {
                        // 1 vs 1: El rival va ARRIBA
                        OpponentTop = CreateOpponent(rivalNick);
                    }
                    else if (totalPlayers == 3)
                    {
                        // 1 vs 2: Izquierda y Derecha (o Arriba)
                        // i=1 (Siguiente) -> Izquierda
                        // i=2 (Último) -> Derecha
                        if (i == 1) OpponentLeft = CreateOpponent(rivalNick);
                        else if (i == 2) OpponentRight = CreateOpponent(rivalNick); // O Top, según prefieras
                    }
                    else // 4 Jugadores
                    {
                        // i=1 -> Izquierda
                        // i=2 -> Arriba (Frente)
                        // i=3 -> Derecha
                        if (i == 1) OpponentLeft = CreateOpponent(rivalNick);
                        else if (i == 2) OpponentTop = CreateOpponent(rivalNick);
                        else if (i == 3) OpponentRight = CreateOpponent(rivalNick);
                    }
                }

                // Notificar cambios a la UI (si tus propiedades usan SetProperty)
                OnPropertyChanged(nameof(OpponentLeft));
                OnPropertyChanged(nameof(OpponentTop));
                OnPropertyChanged(nameof(OpponentRight));
            });
        }

        private OpponentModel CreateOpponent(string nickname)
        {
            // Aquí deberías recuperar el Avatar real si puedes (Fase 2), 
            // por ahora usa un placeholder o intenta sacarlo de cache local si lo tienes.
            return new OpponentModel
            {
                Nickname = nickname,
                CardCount = 7, // Todos empiezan con 7
                AvatarImagePath = "pack://application:,,,/Avatars/LogoUNO.png" // Placeholder
            };
        }

        private void ExecuteUseItem(ItemType itemType)
        {
            SoundManager.PlayClick();
            var item = Items.FirstOrDefault(i => i.Type == itemType);
            if (item != null)
            {
                item.Count--;
                item.UpdateCanExecute();
            }
            _dialogService.ShowAlert(Match.ItemUsedLabel, string.Format(Match.ItemUsedMessageLabel, CurrentTurnNickname, item.Type), PopUpIconType.Info);
        }

        private void UpdatePlayableCards()
        {
            foreach (var card in PlayerHand)
            {
                card.IsPlayable = true;
                card.UpdateCanExecute();
            }
        }

        private void ExecuteToggleSettings()
        {
            SoundManager.PlayClick();
            IsSettingsMenuVisible = !IsSettingsMenuVisible;
        }

        private void ExecuteExitGame()
        {
            _navigationService.NavigateTo(new MainMenuPage());
        }

        private void ExecuteReportPlayer()
        {
            // TODO: Implement report player functionality
        }
    }
}
