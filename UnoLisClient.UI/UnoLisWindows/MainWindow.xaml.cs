using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace UnoLisClient.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VideoBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 🔁 Repite el video en bucle
            VideoBackground.Position = TimeSpan.Zero;
            VideoBackground.Play();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 🎵 Inicia música y navega a la primera página (GamePage)
            MainFrame.Navigate(new Pages.GamePage());

            // 🟣 Prepara pantalla negra y logo
            IntroMask.Visibility = Visibility.Visible;
            IntroMask.Opacity = 1;
            SplashLogo.Visibility = Visibility.Visible;
            SplashLogo.Opacity = 0;

            // 🟡 Etapa 1: Fade-in + scale del logo UNO
            var fadeInLogo = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(3.0),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            var scaleLogo = new DoubleAnimation
            {
                From = 0.8,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(3.0),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeInLogo);
            SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleLogo);
            SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleLogo);

            await Task.Delay(3000); // Logo visible unos segundos

            // Etapa 2: Fade-out del logo UNO
            var fadeOutLogo = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.8),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeOutLogo);

            await Task.Delay(2000);
            MusicPlayer.Play();

            // Etapa 3: Fade-out del fondo negro (IntroMask)
            var fadeOutMask = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            fadeOutMask.Completed += (s, _) =>
            {
                IntroMask.Visibility = Visibility.Collapsed;
                SplashLogo.Visibility = Visibility.Collapsed;
            };

            IntroMask.BeginAnimation(UIElement.OpacityProperty, fadeOutMask);
        }

        private void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 🔁 Repite música de fondo
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }

        // 🎬 Transición cinematográfica al cambiar video/música
        public async void SetBackgroundMedia(string videoPath, string musicPath)
        {
            try
            {
                // 🔸 Paso 1: Fade out suave (video + audio)
                var fadeOut = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1.0),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };

                VideoBackground.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                double oldVolume = MusicPlayer.Volume;

                var fadeOutMusic = new DoubleAnimation
                {
                    From = oldVolume,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1.0),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeOutMusic);

                await Task.Delay(1000);

                // 🔸 Paso 2: Cambiar las fuentes
                VideoBackground.Stop();
                MusicPlayer.Stop();

                VideoBackground.Source = new Uri(System.IO.Path.GetFullPath(videoPath));
                MusicPlayer.Source = new Uri(System.IO.Path.GetFullPath(musicPath));

                // 🔸 Paso 3: Reproducir nuevos medios
                VideoBackground.Play();
                MusicPlayer.Play();

                // 🔸 Paso 4: Fade in suave (video + audio)
                var fadeIn = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1.0),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                VideoBackground.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                var fadeInMusic = new DoubleAnimation
                {
                    From = 0,
                    To = oldVolume,
                    Duration = TimeSpan.FromSeconds(1.0),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeInMusic);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing background media: {ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void RestoreDefaultBackground()
        {
            try
            {
                string defaultVideo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/marioDesktop.mp4");
                string defaultMusic = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/jazzBackground.mp3");

                // 🟣 Fade a negro

                await Task.Delay(600);

                VideoBackground.Stop();
                MusicPlayer.Stop();

                VideoBackground.Source = new Uri(defaultVideo, UriKind.Absolute);
                MusicPlayer.Source = new Uri(defaultMusic, UriKind.Absolute);

                VideoBackground.Play();
                MusicPlayer.Play();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring background:\n{ex.Message}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

}