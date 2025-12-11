using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchResultsViewModel : BaseViewModel
    {
        private const string VictorySoundFile = "victoryMatch.mp3";
        private const string DefaultAvatar = "LogoUNO";
        private const string AvatarsBasePathFormat = "pack://application:,,,/Avatars/";
        private const string AvatarFileExtension = ".png";

        private readonly INavigationService _navigationService;

        public ObservableCollection<ResultDisplayModel> Losers { get; set; }
        public ResultDisplayModel Winner { get; set; }
        public ICommand BackToMenuCommand { get; }

        public MatchResultsViewModel(INavigationService navigationService, List<ResultData> results)
            : base(new AlertManager())
        {
            _navigationService = navigationService;
            BackToMenuCommand = new RelayCommand(ExecuteBackToMenu);

            ProcessMatchResults(results);
            SoundManager.PlaySound(VictorySoundFile);
        }

        private void ProcessMatchResults(List<ResultData> rawResults)
        {
            if (rawResults == null || rawResults.Count == 0)
            {
                return;
            }

            var orderedResults = rawResults.OrderBy(r => r.Rank).ToList();
            var visualList = new List<ResultDisplayModel>();

            foreach (var data in orderedResults)
            {
                visualList.Add(CreateDisplayModel(data));
            }

            Winner = visualList.First();
            Losers = new ObservableCollection<ResultDisplayModel>(visualList.Skip(1));
        }

        private ResultDisplayModel CreateDisplayModel(ResultData data)
        {
            string avatarName = string.IsNullOrEmpty(data.AvatarName) ? DefaultAvatar : data.AvatarName;
            string fullPath = $"{AvatarsBasePathFormat}{avatarName}{AvatarFileExtension}";

            return new ResultDisplayModel
            {
                Nickname = data.Nickname,
                Rank = data.Rank,
                Score = data.Score,
                AvatarImagePath = fullPath
            };
        }

        private void ExecuteBackToMenu()
        {
            _navigationService.NavigateTo(new MainMenuPage());
        }
    }

    public class ResultDisplayModel
    {
        public string Nickname { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public string AvatarImagePath { get; set; }
    }
}