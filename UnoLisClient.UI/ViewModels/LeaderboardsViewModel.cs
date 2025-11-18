using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;

namespace UnoLisClient.UI.ViewModels
{
    public class LeaderboardsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly Page _view;

        public ObservableCollection<LeaderboardEntry> LeaderboardEntries { get; }
            = new ObservableCollection<LeaderboardEntry>();

        public ICommand LoadLeaderboardsCommand { get; }
        public ICommand BackCommand { get; }

        public LeaderboardsViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;

            LoadLeaderboardsCommand = new RelayCommand(LoadLeaderboardData);
            BackCommand = new RelayCommand(ExecuteBack);

            LoadLeaderboardData();
        }

        // Simmulated loading of leaderboard data
        private void LoadLeaderboardData()
        {
            var entries = new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Rank = 1, PlayerName = "SweetBlue16", Score = 1500, MatchesPlayed = 120, WinRate = "72%" },
                new LeaderboardEntry { Rank = 2, PlayerName = "MapleVR", Score = 1400, MatchesPlayed = 100, WinRate = "68%" },
                new LeaderboardEntry { Rank = 3, PlayerName = "Maverick", Score = 1300, MatchesPlayed = 150, WinRate = "61%" },
                new LeaderboardEntry { Rank = 4, PlayerName = "IngeAbraham", Score = 1200, MatchesPlayed = 95, WinRate = "64%" },
                new LeaderboardEntry { Rank = 5, PlayerName = "PlayerFive", Score = 1100, MatchesPlayed = 110, WinRate = "58%" },
            };
            LeaderboardEntries.Clear();
            foreach (var entry in entries)
            {
                LeaderboardEntries.Add(entry);
            }
        }

        private void ExecuteBack()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }
    }
}
