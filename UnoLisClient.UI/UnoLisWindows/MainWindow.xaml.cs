using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.Pages;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.UnoLisServerReference.Logout;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;

namespace UnoLisClient.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILogoutManagerCallback
    {
        private LogoutManagerClient _logoutClient;

        public MainWindow()
        {
            InitializeComponent();
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

        private void VideoBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoBackground.Position = TimeSpan.Zero;
            VideoBackground.Play();
        }

        public void SetMusicVolume(double volume)
        {
            MusicPlayer.Volume = volume / 100.0;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Navigate(new Pages.GamePage());
                IntroMask.Visibility = Visibility.Visible;
                IntroMask.Opacity = 1;
                SplashLogo.Visibility = Visibility.Visible;
                SplashLogo.Opacity = 0;

                var fadeInLogo = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(3.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                var scaleLogo = new DoubleAnimation { From = 0.8, To = 1.0, Duration = TimeSpan.FromSeconds(3.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };

                SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeInLogo);
                SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleLogo);
                SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleLogo);

                await Task.Delay(3000);

                var fadeOutLogo = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(1.8), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeOutLogo);

                await Task.Delay(2000);
                MusicPlayer.Play();

                var fadeOutMask = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(2.5), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };

                fadeOutMask.Completed += (s, _) =>
                {
                    IntroMask.Visibility = Visibility.Collapsed;
                    SplashLogo.Visibility = Visibility.Collapsed;
                };

                IntroMask.BeginAnimation(UIElement.OpacityProperty, fadeOutMask);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Operación inválida en la animación de inicio: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show($"Tarea cancelada durante la animación: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Elemento no inicializado en la ventana principal: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar la ventana principal: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }

        public async void SetBackgroundMedia(string videoPath, string musicPath)
        {
            try
            {
                var fadeOut = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                VideoBackground.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                double oldVolume = MusicPlayer.Volume;

                var fadeOutMusic = new DoubleAnimation { From = oldVolume, To = 0, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeOutMusic);

                await Task.Delay(1000);

                VideoBackground.Stop();
                MusicPlayer.Stop();

                VideoBackground.Source = new Uri(System.IO.Path.GetFullPath(videoPath));
                MusicPlayer.Source = new Uri(System.IO.Path.GetFullPath(musicPath));

                VideoBackground.Play();
                MusicPlayer.Play();

                var fadeIn = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                VideoBackground.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                var fadeInMusic = new DoubleAnimation { From = 0, To = oldVolume, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeInMusic);
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show($"Ruta de archivo inválida: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show($"Archivo multimedia no encontrado: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Operación no válida en la transición de medios: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cambiando medios de fondo: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void RestoreDefaultBackground()
        {
            try
            {
                string defaultVideo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/marioDesktop.mp4");
                string defaultMusic = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/jazzBackground.mp3");

                await Task.Delay(600);

                VideoBackground.Stop();
                MusicPlayer.Stop();

                VideoBackground.Source = new Uri(defaultVideo, UriKind.Absolute);
                MusicPlayer.Source = new Uri(defaultMusic, UriKind.Absolute);

                VideoBackground.Play();
                MusicPlayer.Play();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show($"Archivo multimedia por defecto no encontrado: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Operación inválida al restaurar el fondo: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restaurando el fondo: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutCurrentUser()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CurrentSession.CurrentUserNickname))
                {
                    var context = new InstanceContext(this);
                    _logoutClient = new LogoutManagerClient(context);
                    _logoutClient.LogoutAsync(CurrentSession.CurrentUserNickname);

                    CurrentSession.CurrentUserNickname = null;
                    CurrentSession.CurrentUserProfileData = null;
                }
            }
            catch (Exception ex)
            {
                new SimplePopUpWindow(Global.UnsuccessfulLabel, ex.Message).ShowDialog();
            }
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = new QuestionPopUpWindow(Global.ConfirmationLabel, Global.LogoutMessageLabel).ShowDialog();
            if (result == true)
            {
                LogoutCurrentUser();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}