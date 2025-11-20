using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
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
        private readonly Page _view;

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
        public ICommand ExitGameCommand { get; }
        public ICommand ReportPlayerCommand { get; }

        public MatchBoardViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;

            DrawCardCommand = new RelayCommand(ExecuteDrawCard);
            CallUnoCommand = new RelayCommand(ExecuteCallUno);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            ExitGameCommand = new RelayCommand(ExecuteExitGame);
            ReportPlayerCommand = new RelayCommand(ExecuteReportPlayer);

            LoadMockData();
        }

        private void LoadMockData()
        {
            OpponentTop = new OpponentModel { Nickname = "MapleVR", AvatarImagePath = "pack://application:,,,/Avatars/Gatito.png", CardCount = 7 };
            OpponentLeft = new OpponentModel { Nickname = "Erickmel", AvatarImagePath = "pack://application:,,,/Avatars/CC2Projector.png", CardCount = 7, IsHost = true };
            OpponentRight = new OpponentModel { Nickname = "IngeAbraham", AvatarImagePath = "pack://application:,,,/Avatars/Palito.png", CardCount = 7 };

            Items.Add(new ItemModel(ItemType.Shield, 1, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.Thief, 3, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.ExtraTurn, 2, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.Swap, 2, (item) => ExecuteUseItem(item)));

            var card1 = new Card()
            {
                Color = CardColor.Red,
                Value = CardValue.Two,
                ImagePath = "pack://application:,,,/Assets/Cards/TwoRedCard.png"
            };
            var card2 = new Card()
            {
                Color = CardColor.Blue,
                Value = CardValue.DrawTwo,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawTwoBlueCard.png"
            };
            var card3 = new Card()
            {
                Color = CardColor.Black,
                Value = CardValue.WildDrawFour,
                ImagePath = "pack://application:,,,/Assets/Cards/WildDrawFourCard.png"
            };
            var card4 = new Card()
            {
                Color = CardColor.Silver,
                Value = CardValue.DrawTen,
                ImagePath = "pack://application:,,,/Assets/Cards/WildDrawTenCard.png"
            };
            var card5 = new Card()
            {
                Color = CardColor.Silver,
                Value = CardValue.WildDrawFourReverse,
                ImagePath = "pack://application:,,,/Assets/Cards/WildDrawSkipReverseCard.png"
            };

            PlayerHand.Clear();
            PlayerHand.Add(new CardModel(card1, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card2, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card3, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card4, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card5, (card) => ExecutePlayCard(card)));

            DiscardPileTopCard = new CardModel(new Card()
            {
                Color = CardColor.Green,
                Value = CardValue.Five,
                ImagePath = "pack://application:,,,/Assets/Cards/FiveGreenCard.png"
            }, null);
            DeckPileCard = new CardModel(new Card()
            {
                Color = CardColor.Black,
                Value = CardValue.Zero,
                ImagePath = "pack://application:,,,/Assets/Cards/BackUNOCard.png"
            }, null);
            CurrentTurnNickname = "SweetBlue16";
            UpdatePlayableCards();
            UpdateUnoButtonStatus();
        }

        private void ExecutePlayCard(CardModel card)
        {
            SoundManager.PlayClick();
            PlayerHand.Remove(card);
            DiscardPileTopCard = card;

            if (PlayerHand.Count == 1)
            {
                IsUnoButtonVisible = true;
            }

            OpponentLeft.CardCount += 2;
            UpdatePlayableCards();
            UpdateUnoButtonStatus();
        }

        private void ExecuteDrawCard()
        {
            SoundManager.PlayClick();
            PlayerHand.Add(new CardModel(new Card()
            {
                Color = CardColor.Yellow,
                Value = CardValue.Seven,
                ImagePath = "pack://application:,,,/Assets/Cards/SevenYellowCard.png"
            }, (card) => ExecutePlayCard(card)));
            UpdatePlayableCards();
            UpdateUnoButtonStatus();
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
            if (_dialogService.ShowQuestionDialog(Global.ConfirmationLabel, Global.LogoutMessageLabel, PopUpIconType.Question))
            {
                _navigationService.NavigateTo(new MainMenuPage());
            }
        }

        private void ExecuteReportPlayer()
        {
            // TODO: Implement report player functionality
        }
    }
}
