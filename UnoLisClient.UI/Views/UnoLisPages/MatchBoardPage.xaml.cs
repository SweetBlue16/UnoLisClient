using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.UnoLisWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for MatchBoardPage.xaml
    /// </summary>
    public partial class MatchBoardPage : Page, INavigationService
    {
        private readonly MatchBoardViewModel _viewModel;
        private readonly string _lobbyCode;
        public MatchBoardPage(string lobbyCode)
        {
            InitializeComponent();
            _lobbyCode = lobbyCode;

            _viewModel = new MatchBoardViewModel(this, new AlertManager());

            _viewModel.RequestSetBackground += OnBackgroundRequested;

            this.DataContext = _viewModel;
        }

        private void MatchBoardPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_lobbyCode))
            {
                _ = _viewModel.InitializeMatchAsync(_lobbyCode);
            }
        }

        private async void OnBackgroundRequested(string videoName)
        {
            string videoPath = $"Assets/{videoName}";
            string musicPath = "Assets/lobbyMusic.mp3";
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                await mainWindow.SetBackgroundMedia(videoPath, musicPath);
            }
        }

        public void NavigateTo(Page page)
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.Navigate(page)
            );
        }

        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.GoBack()
            );
        }
    }
}
