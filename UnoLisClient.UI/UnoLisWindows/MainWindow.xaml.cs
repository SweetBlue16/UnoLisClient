using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

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
            VideoBackground.Position = TimeSpan.Zero;
            VideoBackground.Play();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 🎵 Inicia música y navega a la primera página (GamePage)
            MainFrame.Navigate(new Pages.GamePage());

            // Aseguramos que ambos estén visibles al inicio
            IntroMask.Visibility = Visibility.Visible;
            IntroMask.Opacity = 1;
            SplashLogo.Visibility = Visibility.Visible;
            SplashLogo.Opacity = 0;

            // 🟡 Etapa 1: Fade-in + scale del logo UNO
            var fadeInLogo = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(3.0),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut
                }
            };

            var scaleLogo = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 0.8,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(3.0),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut
                }
            };

            SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeInLogo);
            SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleLogo);
            SplashLogo.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleLogo);

            await Task.Delay(3000); // Logo visible unos segundos

            // 🟠 Etapa 2: Fade-out del logo UNO
            var fadeOutLogo = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.8),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut
                }
            };
            SplashLogo.BeginAnimation(UIElement.OpacityProperty, fadeOutLogo);

            await Task.Delay(2000);
            MusicPlayer.Play();

            // 🔵 Etapa 3: Fade-out del fondo negro (IntroMask)
            var fadeOutMask = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2.5),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut
                }
            };

            // Cuando termina, ocultamos todo el overlay
            fadeOutMask.Completed += (s, _) =>
            {
                IntroMask.Visibility = Visibility.Collapsed;
                SplashLogo.Visibility = Visibility.Collapsed;
            };

            IntroMask.BeginAnimation(UIElement.OpacityProperty, fadeOutMask);
        }

        private void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }

    }

}
