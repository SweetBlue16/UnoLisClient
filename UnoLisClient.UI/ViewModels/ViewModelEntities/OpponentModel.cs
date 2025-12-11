namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class OpponentModel : ObservableObject
    {
        private const int SkippedAnimationDurationMs = 2000;

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

        private bool _isConnected = true;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }
        public async void TriggerSkipAnimation()
        {
            IsSkipped = true;
            await System.Threading.Tasks.Task.Delay(SkippedAnimationDurationMs);
            IsSkipped = false;
        }
    }
}
