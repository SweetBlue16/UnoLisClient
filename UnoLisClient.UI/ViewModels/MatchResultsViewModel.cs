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
        private readonly INavigationService _navigationService;

        public ObservableCollection<ResultData> Results { get; set; }

        public ResultData Winner { get; set; }

        public ICommand BackToMenuCommand { get; }

        public MatchResultsViewModel(INavigationService navigationService, List<ResultData> results)
            : base(new AlertManager())
        {
            _navigationService = navigationService;
            BackToMenuCommand = new RelayCommand(ExecuteBackToMenu);

            var orderedResults = results.OrderBy(r => r.Rank).ToList();
            Winner = orderedResults.FirstOrDefault();

            Results = new ObservableCollection<ResultData>(orderedResults);
        }

        private void ExecuteBackToMenu()
        {
            _navigationService.NavigateTo(new MainMenuPage());
        }
    }
}