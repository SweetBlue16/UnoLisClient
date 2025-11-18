namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class OpponentModel : ObservableObject
    {
        public string Nickname { get; set; }
        public string AvatarImagePath { get; set; }
        public bool IsHost { get; set; }

        private int _cardCount;
        public int CardCount
        {
            get => _cardCount;
            set => SetProperty(ref _cardCount, value);
        }

        private bool _isTheirTurn;
        public bool IsTheirTurn
        {
            get => _isTheirTurn;
            set => SetProperty(ref _isTheirTurn, value);
        }
    }
}
