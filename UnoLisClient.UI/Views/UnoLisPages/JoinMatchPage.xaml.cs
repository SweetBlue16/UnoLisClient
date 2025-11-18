using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Utilities;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for JoinMatchPage.xaml
    /// </summary>
    public partial class JoinMatchPage : Page, INavigationService
    {
        private readonly JoinMatchViewModel _viewModel;
        public JoinMatchPage()
        {
            InitializeComponent();

            _viewModel = new JoinMatchViewModel(
                this,
                new AlertManager(),
                MatchmakingService.Instance
            );

            this.DataContext = _viewModel;
        }

        public void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }

        public void GoBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}

