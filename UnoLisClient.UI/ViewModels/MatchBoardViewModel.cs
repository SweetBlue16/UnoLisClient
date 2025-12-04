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
using System.Windows.Threading;
using System.Windows.Media;
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
        private readonly DispatcherTimer _localTurnTimer;
        private readonly Page _view;

        private const int TurnDuration = 30;

        public event Action<string> RequestSetBackground;

        private string _currentLobbyCode;
        private string _currentUserNickname;
        private List<GamePlayer> _allPlayersData;
        private CardModel _pendingWildCard;
        private OpponentModel _opponentTop;
        private OpponentModel _opponentLeft;
        private OpponentModel _opponentRight;

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

        public OpponentModel OpponentTop
        {
            get => _opponentTop;
            set => SetProperty(ref _opponentTop, value);
        }

        public OpponentModel OpponentLeft
        {
            get => _opponentLeft;
            set => SetProperty(ref _opponentLeft, value);
        }

        public OpponentModel OpponentRight
        {
            get => _opponentRight;
            set => SetProperty(ref _opponentRight, value);
        }

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

        private bool _isColorSelectorVisible;
        public bool IsColorSelectorVisible
        {
            get => _isColorSelectorVisible;
            set => SetProperty(ref _isColorSelectorVisible, value);
        }

        private string _currentTurnAvatarPath;
        public string CurrentTurnAvatarPath
        {
            get => _currentTurnAvatarPath;
            set => SetProperty(ref _currentTurnAvatarPath, value);
        }

        private int _currentTurnSeconds;
        public int CurrentTurnSeconds
        {
            get => _currentTurnSeconds;
            set => SetProperty(ref _currentTurnSeconds, value);
        }

        private bool _isTimerWarning;
        public bool IsTimerWarning
        {
            get => _isTimerWarning;
            set => SetProperty(ref _isTimerWarning, value);
        }

        public ICommand DrawCardCommand { get; }
        public ICommand CallUnoCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand LeaveMatchCommand { get; }
        public ICommand ReportPlayerCommand { get; }
        public ICommand SelectColorCommand { get; }

        public MatchBoardViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _matchmakingService = MatchmakingService.Instance;
            _gameplayService = GameplayService.Instance;

            _currentUserNickname = Logic.Models.CurrentSession.CurrentUserNickname;
            _gameplayService.Initialize(_currentUserNickname);

            _localTurnTimer = new DispatcherTimer();
            _localTurnTimer.Interval = TimeSpan.FromSeconds(1);
            _localTurnTimer.Tick += LocalTurnTimer_Tick;

            DrawCardCommand = new RelayCommand(ExecuteDrawCard);
            CallUnoCommand = new RelayCommand(ExecuteCallUno);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            LeaveMatchCommand = new RelayCommand(ExecuteExitGame);
            ReportPlayerCommand = new RelayCommand(ExecuteReportPlayer);
            SelectColorCommand = new RelayCommand<string>(ExecuteSelectColor);

            PlayerHand = new ObservableCollection<CardModel>();
        }

        public async Task InitializeMatchAsync(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode)) return;

            _currentLobbyCode = lobbyCode;

            System.Diagnostics.Debug.WriteLine($"[CLIENT] Initializing match for: {lobbyCode}");

            await Task.Delay(1000);

            try
            {
                _gameplayService.InitialHandReceived += HandleInitialHand;
                _gameplayService.CardsReceived += HandleCardsReceived;
                _gameplayService.PlayerPlayedCard += HandlePlayerPlayedCard;
                _gameplayService.PlayerDrewCard += HandlePlayerDrewCard;
                _gameplayService.TurnChanged += HandleTurnChanged;
                _gameplayService.PlayerListReceived += HandlePlayerListReceived;
                _gameplayService.GameMessageReceived += HandleGameMessage;
                _gameplayService.GameEnded += HandleGameEnded;

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
                System.Diagnostics.Debug.WriteLine($"[CLIENT] FATAL INIT ERROR: {ex.Message}");
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

                if (_allPlayersData != null)
                {
                    var currentPlayerObj = _allPlayersData.FirstOrDefault(p => p.Nickname == nickname);
                    string avatarName = currentPlayerObj?.AvatarName ?? "LogoUNO";

                    CurrentTurnAvatarPath = $"pack://application:,,,/Avatars/{avatarName}.png";
                }

                _localTurnTimer.Stop();
                CurrentTurnSeconds = TurnDuration;
                IsTimerWarning = false;
                _localTurnTimer.Start();

                foreach (var card in PlayerHand)
                {
                    card.IsPlayable = true; // Falta validar color/valor (lo vemos en Fase 2)
                    card.UpdateCanExecute();
                }

                // Actualizar UI de oponentes (borde dorado, etc.)
                // UpdateOpponentTurnVisuals(nickname); 
            });
        }

        private void HandleGameMessage(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _dialogService.ShowAlert("Aviso de Juego", message, PopUpIconType.Info);
                SoundManager.PlaySound("notification.mp3");
            });
        }

        private void HandleGameEnded(List<ResultData> results)
        {
            System.Diagnostics.Debug.WriteLine("[CLIENT] GAME OVER RECEIVED!");

            _localTurnTimer.Stop();

            CleanupEvents();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _navigationService.NavigateTo(new MatchResultsPage(results));
            });
        }

        private void LocalTurnTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentTurnSeconds > 0)
            {
                CurrentTurnSeconds--;
                IsTimerWarning = CurrentTurnSeconds <= 10;

                if (CurrentTurnSeconds <= 5)
                {
                    // Opcional: Sonido de tictac
                    // SoundManager.PlaySound("tick.mp3"); 
                }
            }
            else
            {
                _localTurnTimer.Stop();
            }
        }

        private async void ExecutePlayCard(CardModel cardModel)
        {
            if (CurrentTurnNickname != _currentUserNickname)
            {
                return;
            }

            
                if (IsWild(cardModel.CardData.Value))
                {
                    _pendingWildCard = cardModel;
                    IsColorSelectorVisible = true;
                    SoundManager.PlayClick();
                    return;
                }
            try
            {

                await _gameplayService.PlayCardAsync(_currentLobbyCode, _currentUserNickname, cardModel.CardData.Id, null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing card: {ex.Message}");
            }
        }

        private async void ExecuteSelectColor(string colorName)
        {
            if (_pendingWildCard == null) return;

            IsColorSelectorVisible = false; // Ocultar popup

            // Convertir string "Red" a int (enum)
            int selectedColorId = (int)CardColor.Red; // Default
            if (Enum.TryParse(colorName, out CardColor parsedColor))
            {
                selectedColorId = (int)parsedColor;
            }

            try
            {
                // Enviar la jugada al servidor CON el color seleccionado
                await _gameplayService.PlayCardAsync(
                    _currentLobbyCode,
                    _currentUserNickname,
                    _pendingWildCard.CardData.Id,
                    selectedColorId);

                _pendingWildCard = null; // Limpiar
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing Wild card: {ex.Message}");
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

        private void HandlePlayerListReceived(List<GamePlayer> players)
        {
            System.Diagnostics.Debug.WriteLine($"[CLIENT] Player List Received: {string.Join(", ", players)}");
            _allPlayersData = players; 
            Application.Current.Dispatcher.Invoke(() =>
            {
                InitializeOpponents();
            });
        }

        private void InitializeOpponents()
        {
            if (_allPlayersData == null || !_allPlayersData.Any(p => p.Nickname == _currentUserNickname))
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                var myPlayerObj = _allPlayersData.First(p => p.Nickname == _currentUserNickname);
                int myIndex = _allPlayersData.IndexOf(myPlayerObj);
                int totalPlayers = _allPlayersData.Count;

                OpponentLeft = null;
                OpponentTop = null;
                OpponentRight = null;

                OnPropertyChanged(nameof(OpponentLeft));
                OnPropertyChanged(nameof(OpponentTop));
                OnPropertyChanged(nameof(OpponentRight));

                System.Diagnostics.Debug.WriteLine($"[CLIENT] Rendering Opponents. Total Players in List: {totalPlayers}");

                for (int i = 1; i < totalPlayers; i++)
                {
                    int rivalIndex = (myIndex + i) % totalPlayers;
                    var rivalData = _allPlayersData[rivalIndex];

                    int duo = 2;
                    int trio = 3;
                    if (totalPlayers == duo)
                    {
                        OpponentTop = CreateOpponent(rivalData);
                    }
                    else if (totalPlayers == trio)
                    {
                        if (i == 1) OpponentLeft = CreateOpponent(rivalData);
                        else if (i == 2) OpponentRight = CreateOpponent(rivalData);
                    }
                    else 
                    {
                        if (i == 1) OpponentLeft = CreateOpponent(rivalData);
                        else if (i == 2) OpponentTop = CreateOpponent(rivalData);
                        else if (i == 3) OpponentRight = CreateOpponent(rivalData);
                    }
                }

                OnPropertyChanged(nameof(OpponentLeft));
                OnPropertyChanged(nameof(OpponentTop));
                OnPropertyChanged(nameof(OpponentRight));
            });
        }

        private OpponentModel CreateOpponent(GamePlayer playerData)
        {
            string avatarName = string.IsNullOrEmpty(playerData.AvatarName) ? "LogoUNO" : playerData.AvatarName;
            return new OpponentModel
            {
                Nickname = playerData.Nickname,
                CardCount = playerData.CardCount,
                AvatarImagePath = $"pack://application:,,,/Avatars/{avatarName}.png"
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
            _dialogService.ShowAlert(Match.ItemUsedLabel, string.Format(Match.ItemUsedMessageLabel, 
                CurrentTurnNickname, item.Type), PopUpIconType.Info);
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
            _localTurnTimer.Stop();
        }

        private void ExecuteReportPlayer()
        {
            // TODO: Implement report player functionality
        }

        private void CleanupEvents()
        {
            _gameplayService.InitialHandReceived += HandleInitialHand;
            _gameplayService.CardsReceived += HandleCardsReceived;
            _gameplayService.PlayerPlayedCard += HandlePlayerPlayedCard;
            _gameplayService.PlayerDrewCard += HandlePlayerDrewCard;
            _gameplayService.TurnChanged += HandleTurnChanged;
            _gameplayService.PlayerListReceived += HandlePlayerListReceived;
            _gameplayService.GameMessageReceived += HandleGameMessage;
            _gameplayService.GameEnded += HandleGameEnded;
        }
    }
}
