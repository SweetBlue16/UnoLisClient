using System.Collections.Generic;
using System.Windows.Controls;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class MatchResultsPage : Page, INavigationService
    {
        public MatchResultsPage(List<ResultData> results)
        {
            InitializeComponent();
            DataContext = new MatchResultsViewModel(this, results);
        }

        public void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }

        public void GoBack() { }
    }
}