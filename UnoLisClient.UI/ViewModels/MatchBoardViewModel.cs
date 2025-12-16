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
using ServiceItemType = UnoLisServer.Common.Enums.ItemType;

using LocalItemType = UnoLisClient.UI.ViewModels.ViewModelEntities.ItemType;

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
        private const int DelayMilliseconds = 1000;
        private const int NumberOfItemsPerType = 1;

        private const string ShuffleSoundPath = "shuffle_deck.mp3";
        private const string CardFlipSoundPath = "card_flip.mp3";
        private const string DefaultAvatar = "LogoUNO";
        private const string AvatarsBasePathFormat = "pack://application:,,,/Avatars/";
        private const string AvatarFileExtension = ".png";
        private const string ButtonClickSoundPath = "buttonClick.mp3";

        public event Action<string> RequestSetBackground;

        private string _currentLobbyCode;
        private string _currentUserNickname;
        private bool _hasShoutedUnoLocal = false;
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

        private SolidColorBrush _currentTableColor = Brushes.Transparent;
        public SolidColorBrush CurrentTableColor
        {
            get => _currentTableColor;
            set => SetProperty(ref _currentTableColor, value);
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

        private bool _isGameClockwise = true;
        public bool IsGameClockwise
        {
            get => _isGameClockwise;
            set => SetProperty(ref _isGameClockwise, value);
        }

        private bool _isWildAnimationActive;
        public bool IsWildAnimationActive
        {
            get => _isWildAnimationActive;
            set => SetProperty(ref _isWildAnimationActive, value);
        }

        private bool _isSkipReverseAnimationActive;
        public bool IsSkipReverseAnimationActive
        {
            get => _isSkipReverseAnimationActive;
            set => SetProperty(ref _isSkipReverseAnimationActive, value);
        }

        private bool _isSwapAnimationActive;
        public bool IsSwapAnimationActive
        {
            get => _isSwapAnimationActive;
            set => SetProperty(ref _isSwapAnimationActive, value);
        }

        public ICommand DrawCardCommand { get; }
        public ICommand CallUnoCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand LeaveMatchCommand { get; }
        public ICommand OpenReportWindowCommand { get; }
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
            OpenReportWindowCommand = new RelayCommand(ExecuteOpenReport);
            ReportPlayerCommand = new RelayCommand(ExecuteReportPlayer);
            SelectColorCommand = new RelayCommand<string>(ExecuteSelectColor);

            PlayerHand = new ObservableCollection<CardModel>();
        }

        public async Task InitializeMatchAsync(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode))
            {
                return;
            }

            _currentLobbyCode = lobbyCode;
            await Task.Delay(DelayMilliseconds);

            try
            {
                UnsubscribeFromGameEvents();
                SubscribeToGameEvents();
                
                await LoadMatchSettingsAsync(lobbyCode);
                await ConnectWithRetriesAsync(lobbyCode);

                LoadItems();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CLIENT] Background error (ignored): {ex.Message}");
            }
        }

        private async Task LoadMatchSettingsAsync(string lobbyCode)
        {
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
        }

        private async Task ConnectWithRetriesAsync(string lobbyCode)
        {
            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] Connecting... (Attempt {4 - retries}/3)");

                    await _gameplayService.ConnectToGameAsync(lobbyCode, _currentUserNickname);

                    System.Diagnostics.Debug.WriteLine("[CLIENT] Connect command sent successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    retries--;
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] Connection failed: {ex.Message}. Retries left: {retries}");

                    if (retries == 0)
                    {
                        throw new Exception("Server unreachable after 3 attempts.", ex);
                    }

                    await Task.Delay(DelayMilliseconds);
                }
            }
        }

        private void UnsubscribeFromGameEvents()
        {
            _gameplayService.InitialHandReceived -= HandleInitialHand;
            _gameplayService.CardsReceived -= HandleCardsReceived;
            _gameplayService.PlayerPlayedCard -= HandlePlayerPlayedCard;
            _gameplayService.PlayerDrewCard -= HandlePlayerDrewCard;
            _gameplayService.TurnChanged -= HandleTurnChanged;
            _gameplayService.PlayerListReceived -= HandlePlayerListReceived;
            _gameplayService.GameMessageReceived -= HandleGameMessage;
            _gameplayService.GameEnded -= HandleGameEnded;
            _gameplayService.PlayerShoutedUnoReceived -= HandlePlayerShoutedUno;
        }

        private void SubscribeToGameEvents()
        {
            _gameplayService.InitialHandReceived += HandleInitialHand;
            _gameplayService.CardsReceived += HandleCardsReceived;
            _gameplayService.PlayerPlayedCard += HandlePlayerPlayedCard;
            _gameplayService.PlayerDrewCard += HandlePlayerDrewCard;
            _gameplayService.TurnChanged += HandleTurnChanged;
            _gameplayService.PlayerListReceived += HandlePlayerListReceived;
            _gameplayService.GameMessageReceived += HandleGameMessage;
            _gameplayService.GameEnded += HandleGameEnded;
            _gameplayService.PlayerShoutedUnoReceived += HandlePlayerShoutedUno;
        }

        private void HandleInitialHand(List<Card> hand)
        {
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
                            System.Diagnostics.Debug.WriteLine($"[CLIENT] Adding visual card:" +
                                $" {cardDto.Color} {cardDto.Value} (ID: {cardDto.Id})");
                            AddCardToHand(cardDto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] UI ERROR Rendering Cards: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            });
        }

        private void HandleCardsReceived(List<Card> cards)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var card in cards)
                {
                    AddCardToHand(card);
                }

                _hasShoutedUnoLocal = false;
                UpdateUnoButtonStatus();
                SoundManager.PlaySound(ShuffleSoundPath);
            });
        }

        private void HandlePlayerPlayedCard(string nickname, Card card, int cardsLeft)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string imagePath = CardAssetHelper.GetImagePath(card.Color, card.Value);
                var displayCard = new Card { Color = card.Color, Value = card.Value, Id = card.Id };
                var playedCardModel = new CardModel(displayCard, null);

                playedCardModel.ImagePath = imagePath;

                DiscardPileTopCard = playedCardModel;

                UpdateTableColor(card.Color);

                if (nickname == _currentUserNickname)
                {
                    var cardToRemove = PlayerHand.FirstOrDefault(cardMatch => cardMatch.CardData.Id == card.Id);
                    if (cardToRemove != null) PlayerHand.Remove(cardToRemove);
                    UpdateUnoButtonStatus();
                }
                else
                {
                    SetOpponentCardCount(nickname, cardsLeft);
                }

                if (IsWild(card.Value))
                {
                    string colorName = GetLocalizedColorName(card.Color);
                    string message = string.Format(Match.ColorChangedMessageLabel, nickname, colorName);

                    IsWildAnimationActive = true;
                }

                if (card.Value == CardValue.Reverse)
                {
                    IsGameClockwise = !IsGameClockwise;
                    IsSkipReverseAnimationActive = true;
                }
                else if (card.Value == CardValue.Skip)
                {
                    IdentifyAndAnimateSkip(nickname);
                    IsSkipReverseAnimationActive = true;
                }

                SoundManager.PlaySound(CardFlipSoundPath);
                UpdateUnoButtonStatus();
            });
        }

        private string GetLocalizedColorName(CardColor color)
        {
            switch (color)
            {
                case CardColor.Red:
                    return Match.ColorRed;
                case CardColor.Blue:
                    return Match.ColorBlue;
                case CardColor.Green:
                    return Match.ColorGreen;
                case CardColor.Yellow:
                    return Match.ColorYellow;
                default:
                    return color.ToString();
            }
        }

        private void IdentifyAndAnimateSkip(string attackerNickname)
        {
            if (_allPlayersData == null || _allPlayersData.Count == 0)
            {
                return;
            }

            var attackerObj = _allPlayersData.FirstOrDefault(p => p.Nickname == attackerNickname);
            if (attackerObj == null)
            {
                return;
            }

            int attackerIndex = _allPlayersData.IndexOf(attackerObj);
            int totalPlayers = _allPlayersData.Count;
            int victimIndex;

            if (IsGameClockwise)
            {
                victimIndex = (attackerIndex + 1) % totalPlayers;
            }
            else
            {
                victimIndex = (attackerIndex - 1 + totalPlayers) % totalPlayers;
            }

            var victimObj = _allPlayersData[victimIndex];
            string victimNickname = victimObj.Nickname;

            if (victimNickname == _currentUserNickname)
            {
                SoundManager.PlayClick();
                return;
            }

            if (OpponentLeft != null && OpponentLeft.Nickname == victimNickname)
            {
                OpponentLeft.TriggerSkipAnimation();
            }
            else if (OpponentTop != null && OpponentTop.Nickname == victimNickname)
            {
                OpponentTop.TriggerSkipAnimation();
            }
            else if (OpponentRight != null && OpponentRight.Nickname == victimNickname)
            {
                OpponentRight.TriggerSkipAnimation();
            }
        }

        private void HandlePlayerDrewCard(string nickname, int cardsLeft)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (nickname != _currentUserNickname)
                {
                    SetOpponentCardCount(nickname, cardsLeft);
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
                    card.IsPlayable = true;
                    card.UpdateCanExecute();
                }

            });
        }

        private void HandleGameMessage(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _dialogService.ShowAlert(Match.GameNotificationMessageLabel, message, PopUpIconType.Info);
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

        private async void ExecuteUseItem(LocalItemType localType)
        {
            ServiceItemType serverType;

            switch (localType)
            {
                case LocalItemType.Swap:
                    serverType = ServiceItemType.SwapHands; 
                    break;
                case LocalItemType.Shield:
                    serverType = ServiceItemType.Shield;
                    break;
                case LocalItemType.Thief:
                    serverType = ServiceItemType.Thief;
                    break;
                default:
                    return; 
            }

            if (CurrentTurnNickname != _currentUserNickname)
            {
                return;
            }

            string target = null;
            int minumumOpponents = 2;

            if (serverType == ServiceItemType.SwapHands)
            {
                if (_allPlayersData.Count == minumumOpponents)
                {
                    target = _allPlayersData.FirstOrDefault(p => p.Nickname != _currentUserNickname)?.Nickname;
                }
                else
                {
                    target = OpponentTop?.Nickname ?? OpponentLeft?.Nickname ?? OpponentRight?.Nickname;

                }
            }

            if (serverType == ServiceItemType.SwapHands)
            {
                IsSwapAnimationActive = true;
            }

            if (target != null)
            {
                try
                {
                    await _gameplayService.UseItemAsync(_currentLobbyCode, _currentUserNickname, serverType, target);

                    var item = Items.FirstOrDefault(i => i.Type == localType);
                    if (item != null && item.Count > 0)
                    {
                        item.Count--;
                        item.UpdateCanExecute();
                    }

                    SoundManager.PlayClick();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[CLIENT] Error using item: {ex.Message}");
                }
            }
        }
        private void LocalTurnTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentTurnSeconds > 0)
            {
                CurrentTurnSeconds--;
                IsTimerWarning = CurrentTurnSeconds <= 10;
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

        private void SetOpponentCardCount(string nickname, int exactCount)
        {
            if (OpponentLeft?.Nickname == nickname)
            {
                OpponentLeft.CardCount = exactCount;
            }
            else if (OpponentTop?.Nickname == nickname)
            {
                OpponentTop.CardCount = exactCount;
            }
            else if (OpponentRight?.Nickname == nickname)
            {
                OpponentRight.CardCount = exactCount;
            }
        }

        private async void ExecuteSelectColor(string colorName)
        {
            if (_pendingWildCard == null) return;

            IsColorSelectorVisible = false;

            int selectedColorId = (int)CardColor.Red;
            var targetColorEnum = CardColor.Red;
            if (Enum.TryParse(colorName, out CardColor parsedColor))
            {
                selectedColorId = (int)parsedColor;
                targetColorEnum = parsedColor;
            }

            UpdateTableColor(targetColorEnum);

            try
            {
                await _gameplayService.PlayCardAsync(
                    _currentLobbyCode,
                    _currentUserNickname,
                    _pendingWildCard.CardData.Id,
                    selectedColorId);

                _pendingWildCard = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing Wild card: {ex.Message}");
            }
        }

        private async void ExecuteDrawCard()
        {
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

        private async void ExecuteCallUno()
        {
            SoundManager.PlayClick();
            _hasShoutedUnoLocal = true;
            UpdateUnoButtonStatus();

            try
            {
                await _gameplayService.SayUnoAsync(_currentLobbyCode, _currentUserNickname);
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine("Timeout while saying UNO.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saying UNO: {ex.Message}");
            }
        }

        private void UpdateUnoButtonStatus()
        {
            IsUnoButtonVisible = (PlayerHand.Count == 1 && !_hasShoutedUnoLocal);
        }

        private void HandlePlayerListReceived(List<GamePlayer> players)
        {
            _allPlayersData = players;
            Application.Current.Dispatcher.Invoke(() =>
            {
                InitializeOpponents();
            });
        }

        private void HandlePlayerShoutedUno(string nickname)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string message = string.Format(Match.PlayerDeclaredUnoMessageLabel, nickname);
                _dialogService.ShowAlert(Global.AppNameLabel, message, PopUpIconType.Info);

                SoundManager.PlaySound(ButtonClickSoundPath);
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
                        if (i == 1)
                        {
                            OpponentLeft = CreateOpponent(rivalData);
                        }
                        else if (i == 2)
                        {
                            OpponentRight = CreateOpponent(rivalData);
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            OpponentLeft = CreateOpponent(rivalData);
                        }
                        else if (i == 2)
                        {
                            OpponentTop = CreateOpponent(rivalData);
                        }
                        else if (i == 3)
                        {
                            OpponentRight = CreateOpponent(rivalData);
                        }
                    }
                }

                OnPropertyChanged(nameof(OpponentLeft));
                OnPropertyChanged(nameof(OpponentTop));
                OnPropertyChanged(nameof(OpponentRight));
            });
        }

        private OpponentModel CreateOpponent(GamePlayer playerData)
        {
            string avatarName = string.IsNullOrEmpty(playerData.AvatarName) ? DefaultAvatar : playerData.AvatarName;
            return new OpponentModel
            {
                Nickname = playerData.Nickname,
                CardCount = playerData.CardCount,
                AvatarImagePath = $"{AvatarsBasePathFormat}{avatarName}{AvatarFileExtension}",
                IsConnected = playerData.IsConnected
            };
        }

        private void LoadItems()
        {
            Items.Clear();

            var Swap = ItemType.Swap;
            var Shield = ItemType.Shield;
            var Thief = ItemType.Thief;

            Items.Add(new ItemModel(Swap, NumberOfItemsPerType, (t) => ExecuteUseItem(t)));

            Items.Add(new ItemModel(Shield, NumberOfItemsPerType, (t) => ExecuteUseItem(t)));

            Items.Add(new ItemModel(Thief, NumberOfItemsPerType, (t) => ExecuteUseItem(t)));
        }

        private void ExecuteToggleSettings()
        {
            SoundManager.PlayClick();
            IsSettingsMenuVisible = !IsSettingsMenuVisible;
        }

        private void ExecuteOpenReport()
        {
            System.Diagnostics.Debug.WriteLine("Reportar jugador clicado");
        }

        private async void ExecuteExitGame()
        {
            _localTurnTimer.Stop();

            if (!string.IsNullOrEmpty(_currentLobbyCode) && !string.IsNullOrEmpty(_currentUserNickname))
            {
                try
                {
                    await _gameplayService.LeaveGameAsync(_currentLobbyCode, _currentUserNickname);
                }
                catch (TimeoutException)
                {
                    System.Diagnostics.Debug.WriteLine("Timeout while leaving game.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error leaving game gracefully: {ex.Message}");
                }
            }
            CleanupEvents();
            _navigationService.NavigateTo(new MainMenuPage());
        }

        private void ExecuteReportPlayer()
        {
        }

        public void CleanupEvents()
        {
            if (_gameplayService != null)
            {
                UnsubscribeFromGameEvents();
            }
        }

        private void UpdateTableColor(UnoLisClient.Logic.UnoLisServerReference.Gameplay.CardColor color)
        {
            switch (color)
            {
                case UnoLisClient.Logic.UnoLisServerReference.Gameplay.CardColor.Red:
                    CurrentTableColor = new SolidColorBrush(Color.FromRgb(255, 85, 85));
                    break;
                case UnoLisClient.Logic.UnoLisServerReference.Gameplay.CardColor.Blue:
                    CurrentTableColor = new SolidColorBrush(Color.FromRgb(85, 85, 255));
                    break;
                case UnoLisClient.Logic.UnoLisServerReference.Gameplay.CardColor.Green:
                    CurrentTableColor = new SolidColorBrush(Color.FromRgb(85, 170, 85));
                    break;
                case UnoLisClient.Logic.UnoLisServerReference.Gameplay.CardColor.Yellow:
                    CurrentTableColor = new SolidColorBrush(Color.FromRgb(255, 170, 0));
                    break;
                default:
                    break;
            }
        }
    }
}
