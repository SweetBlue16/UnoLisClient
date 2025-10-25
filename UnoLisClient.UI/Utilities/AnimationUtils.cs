using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace UnoLisClient.UI.Utilities
{
    public static class AnimationUtils
    {
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
    }
}