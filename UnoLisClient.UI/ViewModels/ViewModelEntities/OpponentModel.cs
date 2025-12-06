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

        private bool _isSkipped;
        public bool IsSkipped
        {
            get => _isSkipped;
            set => SetProperty(ref _isSkipped, value);
        }
        public async void TriggerSkipAnimation()
        {
            IsSkipped = true;
            await System.Threading.Tasks.Task.Delay(2000);
            IsSkipped = false;
        }
    }
}
