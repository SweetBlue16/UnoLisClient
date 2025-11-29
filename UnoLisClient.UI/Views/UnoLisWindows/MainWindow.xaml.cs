using System;
using System.Threading.Tasks;
using System.Windows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using System.IO;
using UnoLisClient.UI.ViewModels;
using System.Windows.Controls;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.Views.UnoLisWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INavigationService
    {
        private readonly MainViewModel _viewModel;
        private bool _isCleanClose = false;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel(this, new AlertManager(), new LogoutService());
            DataContext = _viewModel;
        }

        public void LogoutResponse(ServiceResponse<object> response)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string message = MessageTranslator.GetMessage(response.Code);
                if (response.Success)
                {
                    new SimplePopUpWindow(Global.SuccessLabel, message).ShowDialog();
                }
                else
                {
                    new SimplePopUpWindow(Global.UnsuccessfulLabel, message).ShowDialog();
                }
            });
        }

        public void NavigateTo(Page page)
        {
            Dispatcher.Invoke(() => MainFrame.Navigate(page));
        }

        public void GoBack()
        {
            Dispatcher.Invoke(() =>
            {
                if (MainFrame.CanGoBack) MainFrame.GoBack();
            });
        }

        public void SetMusicVolume(double volume)
        {
            MusicPlayer.Volume = volume / 100.0;
        }

        public async Task SetBackgroundMedia(string videoPath, string musicPath)
        {
            try
            {
                var mediaArgs = new AnimationUtils.CrossfadeMediaArgs(
                    VideoBackground, MusicPlayer, videoPath, musicPath
                );

                await AnimationUtils.CrossfadeMediaAsync(mediaArgs);
            }
            catch (UriFormatException ex)
            {
                new SimplePopUpWindow(Global.UriFormatLabel, ex.Message).ShowDialog();
            }
            catch (FileNotFoundException ex)
            {
                new SimplePopUpWindow(Global.FileNotFoundLabel, ex.Message).ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                new SimplePopUpWindow(Global.InvalidOperationLabel, ex.Message).ShowDialog();
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }

        public async Task RestoreDefaultBackground()
        {
            try
            {
                await AnimationUtils.RestoreDefaultMediaAsync(VideoBackground, MusicPlayer);
            }
            catch (FileNotFoundException ex)
            {
                new SimplePopUpWindow(Global.FileNotFoundLabel, ex.Message).ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                new SimplePopUpWindow(Global.InvalidOperationLabel, ex.Message).ShowDialog();
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.NavigateToInitialPage();

                var introArgs = new AnimationUtils.IntroAnimationArgs(IntroMask, SplashLogo, 
                    MusicPlayer
                );

                await AnimationUtils.PlayIntroAnimationAsync(introArgs);
            }
            catch (InvalidOperationException ex)
            {
                new SimplePopUpWindow(Global.InvalidOperationLabel, ex.Message).ShowDialog();
            }
            catch (TaskCanceledException ex)
            {
                new SimplePopUpWindow(Global.TaskCanceledLabel, ex.Message).ShowDialog();
            }
            catch (NullReferenceException ex)
            {
                new SimplePopUpWindow(Global.NullReferenceAnimation, ex.Message).ShowDialog();
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }

        private async void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs close)
        {
            if (_isCleanClose)
            {
                return;
            }

            close.Cancel = true;
            bool canClose = await _viewModel.TryLogoutAndCloseAsync();

            if (canClose)
            {
                _isCleanClose = true;

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.Close();
                });
            }
        }

        private void VideoBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoBackground.Position = TimeSpan.Zero;
            VideoBackground.Play();
        }

        private void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }
    }
}