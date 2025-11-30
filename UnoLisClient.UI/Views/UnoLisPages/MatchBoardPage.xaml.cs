using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.Controls;
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
            DataContext = _viewModel;
            _viewModel.PropertyChanged += (s, e) => ViewModel_PropertyChanged(s, e);
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

        private void CloseRequestedSettingsModal(object sender, EventArgs e)
        {
            if (_viewModel.IsSettingsMenuVisible)
            {
                _viewModel.ToggleSettingsCommand.Execute(null);
            }
        }

        private void MatchBoardPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_lobbyCode))
            {
                AnimationUtils.FadeIn(this.Content as Grid, 0.8);
                _ = _viewModel.InitializeMatchAsync(_lobbyCode);
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MatchBoardViewModel.IsSettingsMenuVisible))
            {
                if (_viewModel.IsSettingsMenuVisible)
                {
                    AnimationUtils.FadeIn(GameSettingsModal);
                }
                else
                {
                    AnimationUtils.FadeOut(GameSettingsModal);
                }
            }
        }

        private async void LeaveMatchRequestedSettingsModal(object sender, EventArgs e)
        {
            await AnimationUtils.FadeOut(GameSettingsModal);
            await AnimationUtils.FadeOutTransition(this.Content as Grid, 0.5);
            _viewModel.LeaveMatchCommand.Execute(null);
        }
    }
}
