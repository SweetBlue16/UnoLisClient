using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UnoLisClient.UI.Utilities
{
    public static class AnimationUtils
    {
        /// <summary>
        /// Parameters for Intro Animation.
        /// </summary>
        public readonly struct IntroAnimationArgs
        {
            public readonly UIElement Mask;
            public readonly FrameworkElement Logo;
            public readonly MediaElement MusicPlayer;

            public IntroAnimationArgs(UIElement mask, FrameworkElement logo, MediaElement musicPlayer)
            {
                Mask = mask;
                Logo = logo;
                MusicPlayer = musicPlayer;
            }
        }

        /// <summary>
        /// Crossfade Media Arguments.
        /// </summary>
        public readonly struct CrossfadeMediaArgs
        {
            public readonly MediaElement VideoPlayer;
            public readonly MediaElement MusicPlayer;
            public readonly string NewVideoPath;
            public readonly string NewMusicPath;

            public CrossfadeMediaArgs(MediaElement video, MediaElement music, string videoPath, string musicPath)
            {
                VideoPlayer = video;
                MusicPlayer = music;
                NewVideoPath = videoPath;
                NewMusicPath = musicPath;
            }
        }

        public static void FadeIn(UIElement element, double duration = 0.3)
        {
            element.Visibility = Visibility.Visible;
            element.Opacity = 0;

            var fade = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(duration),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            element.BeginAnimation(UIElement.OpacityProperty, fade);
        }

        public static async Task FadeOut(UIElement element, double duration = 0.25)
        {
            var fade = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(duration),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            element.BeginAnimation(UIElement.OpacityProperty, fade);
            await Task.Delay((int)(duration * 1000));
            element.Visibility = Visibility.Collapsed;
        }

        public static async Task FadeOutTransition(UIElement element, double duration = 0.8)
        {
            if (element == null) return;

            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(duration),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            element.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            await Task.Delay((int)(duration * 1000));
        }

        /// <summary>
        /// Executes the intro animation sequence.
        /// </summary>
        public static async Task PlayIntroAnimationAsync(IntroAnimationArgs args)
        {
            args.Mask.Visibility = Visibility.Visible;
            args.Mask.Opacity = 1;
            args.Logo.Visibility = Visibility.Visible;
            args.Logo.Opacity = 0;

            var fadeInLogo = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(3.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            var scaleLogo = new DoubleAnimation { From = 0.8, To = 1.0, Duration = TimeSpan.FromSeconds(3.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };

            args.Logo.BeginAnimation(UIElement.OpacityProperty, fadeInLogo);
            args.Logo.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleLogo);
            args.Logo.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleLogo);

            await Task.Delay(3000);

            var fadeOutLogo = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(1.8), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            args.Logo.BeginAnimation(UIElement.OpacityProperty, fadeOutLogo);

            await Task.Delay(2000);
            args.MusicPlayer.Play();

            var fadeOutMask = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(2.5), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };

            fadeOutMask.Completed += (s, _) =>
            {
                args.Mask.Visibility = Visibility.Collapsed;
                args.Logo.Visibility = Visibility.Collapsed;
            };

            args.Mask.BeginAnimation(UIElement.OpacityProperty, fadeOutMask);
        }

        /// <summary>
        ///Makes a crossfade transition between two media sources.
        /// </summary>

        public static async Task CrossfadeMediaAsync(CrossfadeMediaArgs args)
        {
            var fadeOut = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            args.VideoPlayer.BeginAnimation(UIElement.OpacityProperty, fadeOut);

            double oldVolume = args.MusicPlayer.Volume;
            var fadeOutMusic = new DoubleAnimation { From = oldVolume, To = 0, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            args.MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeOutMusic);

            await Task.Delay(1000);

            args.VideoPlayer.Stop();
            args.MusicPlayer.Stop();

            string newVideoPathAbsolute = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, args.NewVideoPath);
            string newMusicPathAbsolute = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, args.NewMusicPath);

            args.VideoPlayer.Source = new Uri(newVideoPathAbsolute, UriKind.Absolute);
            args.MusicPlayer.Source = new Uri(newMusicPathAbsolute, UriKind.Absolute);

            args.VideoPlayer.Play();
            args.MusicPlayer.Play();

            var fadeIn = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            args.VideoPlayer.BeginAnimation(UIElement.OpacityProperty, fadeIn);

            var fadeInMusic = new DoubleAnimation { From = 0, To = oldVolume, Duration = TimeSpan.FromSeconds(1.0), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
            args.MusicPlayer.BeginAnimation(MediaElement.VolumeProperty, fadeInMusic);
        }

        public static async Task RestoreDefaultMediaAsync(MediaElement videoPlayer, MediaElement musicPlayer)
        {
            string defaultVideo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/marioDesktop.mp4");
            string defaultMusic = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/jazzBackground.mp3");

            await Task.Delay(600);

            videoPlayer.Stop();
            musicPlayer.Stop();

            videoPlayer.Source = new Uri(defaultVideo, UriKind.Absolute);
            musicPlayer.Source = new Uri(defaultMusic, UriKind.Absolute);

            videoPlayer.Play();
            musicPlayer.Play();
        }
    }
}
