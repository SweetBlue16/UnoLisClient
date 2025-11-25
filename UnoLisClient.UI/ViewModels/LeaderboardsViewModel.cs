using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;

namespace UnoLisClient.UI.ViewModels
{
    public class LeaderboardsViewModel : BaseViewModel
    {
        private readonly ILeaderboardsService _leaderboardsService;
        private readonly INavigationService _navigationService;

        private ObservableCollection<LeaderboardEntry> _topPlayers;

        public ObservableCollection<LeaderboardEntry> LeaderboardEntries
        {
            get => _topPlayers;
            set { _topPlayers = value; OnPropertyChanged(); }
        }

        public RelayCommand BackCommand { get; }

        public LeaderboardsViewModel(ILeaderboardsService leaderboardsService,
        INavigationService navigationService, IDialogService dialogService)
        :base(dialogService)
        {
            _leaderboardsService = leaderboardsService;
            _navigationService = navigationService;

            LeaderboardEntries = new ObservableCollection<LeaderboardEntry>();
            BackCommand = new RelayCommand(ExecuteGoBack);
        }

        public async Task LoadLeaderboardData()
        {
            IsLoading = true;
            LeaderboardEntries.Clear();

            var response = await _leaderboardsService.GetGlobalLeaderboardAsync();

            if (response.Success && response.Data != null)
            {
                foreach (var entry in response.Data)
                {
                    LeaderboardEntries.Add(new LeaderboardEntry
                    {
                        Rank = entry.Rank,
                        PlayerName = entry.Nickname,
                        Score = entry.GlobalPoints,
                        MatchesPlayed = entry.MatchesPlayed,
                        WinRate = entry.WinRate
                    });
                }
            }
            else
            {
                _dialogService.ShowWarning(ErrorMessages.CouldNotLoadRankingMessageLabel);
            }
            IsLoading = false;
        }

        public void ExecuteGoBack()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }
    }
}
