using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchBoardViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly Page _view;

        private CardModel _discardPileTopCard;
        public CardModel DiscardPileTopCard 
        { 
            get => _discardPileTopCard; 
            set => SetProperty(ref _discardPileTopCard, value);
        }

        private string _currentTurnNickname = "SweetBlue16";
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

        private bool _isUnoButtonVisible = true;
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

        public MatchBoardViewModel(Page view, IDialogService dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            _dialogService = dialogService;

            DrawCardCommand = new RelayCommand(ExecuteDrawCard);
            CallUnoCommand = new RelayCommand(ExecuteCallUno);
            ToggleSettingsCommand = new RelayCommand(ExecuteToggleSettings);
            ExitGameCommand = new RelayCommand(ExecuteExitGame);
            ReportPlayerCommand = new RelayCommand(ExecuteReportPlayer);

            LoadMockData();
        }

        private void LoadMockData()
        {
            OpponentTop = new OpponentModel { Nickname = "MapleVR", AvatarImagePath = "...", CardCount = 7 };
            OpponentLeft = new OpponentModel { Nickname = "Erickmel", AvatarImagePath = "...", CardCount = 7, IsHost = true };
            OpponentRight = new OpponentModel { Nickname = "IngeAbraham", AvatarImagePath = "...", CardCount = 7 };

            Items.Add(new ItemModel(ItemType.Shield, 1, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.Thief, 3, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.ExtraTurn, 2, (item) => ExecuteUseItem(item)));
            Items.Add(new ItemModel(ItemType.Swap, 2, (item) => ExecuteUseItem(item)));

            var card1 = new Card()
            {
                Color = CardColor.Red,
                Value = CardValue.Two,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            };
            var card2 = new Card()
            {
                Color = CardColor.Blue,
                Value = CardValue.DrawTwo,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            };
            var card3 = new Card()
            {
                Color = CardColor.Black,
                Value = CardValue.WildDrawFour,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            };
            var card4 = new Card()
            {
                Color = CardColor.Silver,
                Value = CardValue.DrawTen,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            };
            var card5 = new Card()
            {
                Color = CardColor.Silver,
                Value = CardValue.WildDrawFourReverse,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            };

            PlayerHand.Add(new CardModel(card1, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card2, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card3, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card4, (card) => ExecutePlayCard(card)));
            PlayerHand.Add(new CardModel(card5, (card) => ExecutePlayCard(card)));

            DiscardPileTopCard = new CardModel(new Card()
            {
                Color = CardColor.Green,
                Value = CardValue.Five,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            }, null);
            CurrentTurnNickname = "SweetBlue16";
            UpdatePlayableCards();
        }

        private void ExecutePlayCard(CardModel card)
        {
            PlayerHand.Remove(card);
            DiscardPileTopCard = card;

            if (PlayerHand.Count == 1)
            {
                IsUnoButtonVisible = true;
            }

            OpponentLeft.CardCount += 2;
            UpdatePlayableCards();
        }

        private void ExecuteDrawCard()
        {
            PlayerHand.Add(new CardModel(new Card()
            {
                Color = CardColor.Yellow,
                Value = CardValue.Seven,
                ImagePath = "pack://application:,,,/Assets/Cards/DrawFour.png"
            }, (card) => ExecutePlayCard(card)));
            UpdatePlayableCards();
        }

        private void ExecuteCallUno()
        {
            IsUnoButtonVisible = true;
        }

        private void ExecuteUseItem(ItemType itemType)
        {
            var item = Items.FirstOrDefault(i => i.Type == itemType);
            if (item != null)
            {
                item.Count--;
                item.UpdateCanExecute();
            }
        }

        private void UpdatePlayableCards()
        {
            foreach (var card in PlayerHand)
            {
                card.IsPlayable = (card.CardData.Color == DiscardPileTopCard.CardData.Color ||
                                   card.CardData.Value == DiscardPileTopCard.CardData.Value ||
                                   card.CardData.Color == CardColor.Black || card.CardData.Color == CardColor.Silver);
                card.UpdateCanExecute();
            }
        }

        private void ExecuteToggleSettings()
        {
            IsSettingsMenuVisible = !IsSettingsMenuVisible;
        }

        private void ExecuteExitGame()
        {
            if (_dialogService.ShowQuestionDialog(Global.ConfirmationLabel, Global.LogoutMessageLabel))
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
