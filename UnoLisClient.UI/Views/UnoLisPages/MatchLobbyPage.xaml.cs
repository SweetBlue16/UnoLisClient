using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.UnoLisWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    public partial class MatchLobbyPage : Page, INavigationService
    {
        private readonly MatchLobbyViewModel _viewModel;

        public MatchLobbyPage(string lobbyCode)
        {
            InitializeComponent();

            _viewModel = new MatchLobbyViewModel(
                this,
                new AlertManager(),
                FriendsService.Instance,
                ChatService.Instance,
                lobbyCode
            );

            this.DataContext = _viewModel;
            this.ChatControl.SetViewModel(_viewModel.ChatVM);

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private async void MatchLobbyPageLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.OnPageLoaded();
            await SetLobbyBackgroundAsync();
            AnimationUtils.FadeIn(this.Content as Grid, 0.8);
        }

        private async void MatchLobbyPageUnloaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.OnPageUnloaded();
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                _ = mainWindow.RestoreDefaultBackground();
            }
            AnimationUtils.FadeIn(this.Content as Grid, 0.8);
        }

        private static async Task SetLobbyBackgroundAsync()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                string bonfireVideoPath = "Assets/bonfireLobbyBackground.mp4";
                string lobbyMusicPath = "Assets/lobbyMusic.mp3";
                await mainWindow.SetBackgroundMedia(bonfireVideoPath, lobbyMusicPath);
            }
        }
        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MatchLobbyViewModel.IsChatVisible))
            {
                if (_viewModel.IsChatVisible)
                {
                    AnimationUtils.FadeIn(ChatControl);
                }
                else
                {
                    await AnimationUtils.FadeOut(ChatControl);
                }
            }
            else if (e.PropertyName == nameof(MatchLobbyViewModel.IsSettingsVisible))
            {
                if (_viewModel.IsSettingsVisible)
                {
                    AnimationUtils.FadeIn(SettingsModal);
                }
                else
                {
                    await AnimationUtils.FadeOut(SettingsModal);
                }
            }
        }

        private void ChatOverlayMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.IsChatVisible)
            {
                _viewModel.ToggleChatCommand.Execute(null);
            }
        }

        private void CloseRequestedSettingsModal(object sender, EventArgs e)
        {
            if (_viewModel.IsSettingsVisible)
            {
                _viewModel.ToggleSettingsCommand.Execute(null);
            }
        }

        private async void LeaveMatchRequestedSettingsModal(object sender, EventArgs e)
        {

            if (_viewModel.IsSettingsVisible)
            {
                _viewModel.ToggleSettingsCommand.Execute(null);
            }
            await AnimationUtils.FadeOut(SettingsModal);
            await AnimationUtils.FadeOutTransition(this.Content as Grid, 0.8);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                _ = mainWindow.RestoreDefaultBackground();
            }

            _viewModel.LeaveMatchCommand.Execute(null);
        }


        public void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }
        public void GoBack()
        {
            NavigationService?.GoBack();
        }
    }
}