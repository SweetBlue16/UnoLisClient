using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for JoinMatchPage.xaml
    /// </summary>
    public partial class JoinMatchPage : Page, INavigationService
    {
        public static readonly Regex LobbyCodeRegex = new Regex("^[a-zA-Z0-9]{5}$");
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

        private void StrongLobbyCodePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = LobbyCodeRegex.IsMatch(e.Text);
        }
    }
}

