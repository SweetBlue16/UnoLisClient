using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace UnoLisClient.UI.Pages
{
    public partial class MatchLobbyPage : Page
    {
        private bool _isChatVisible = false;
        private bool _isSettingsVisible = false;

        public MatchLobbyPage()
        {
            InitializeComponent();
        }

        // 💬 Botón de Chat
        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isChatVisible)
            {
                FadeOut(ChatPopup);
                _isChatVisible = false;
            }
            else
            {
                FadeIn(ChatPopup);
                _isChatVisible = true;
            }
        }

        // ⚙️ Botón de Ajustes
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isSettingsVisible)
                return;

            _isSettingsVisible = true;
            ShowSettingsModal();
        }

        // 🟢 Animación Fade In
        private void FadeIn(UIElement element)
        {
            element.Visibility = Visibility.Visible;

            var sb = new Storyboard();
            var fade = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fade, element);
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));
            sb.Children.Add(fade);
            sb.Begin();
        }

        // 🔴 Animación Fade Out
        private async void FadeOut(UIElement element)
        {
            var sb = new Storyboard();
            var fade = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            Storyboard.SetTarget(fade, element);
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));
            sb.Children.Add(fade);
            sb.Begin();

            await Task.Delay(250);
            element.Visibility = Visibility.Collapsed;
        }

        // ⚙️ Modal de Ajustes
        private void ShowSettingsModal()
        {
            var overlay = new Grid
            {
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 0, 0, 0)),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            var border = new Border
            {
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(230, 30, 30, 30)),
                CornerRadius = new CornerRadius(10),
                Width = 350,
                Height = 280,
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var stack = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var title = new TextBlock
            {
                Text = "Settings",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Foreground = System.Windows.Media.Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var volumeLabel = new TextBlock
            {
                Text = "Volume",
                FontSize = 16,
                Foreground = System.Windows.Media.Brushes.White
            };

            var volumeSlider = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 50,
                Width = 200,
                Margin = new Thickness(0, 10, 0, 20)
            };

            var exitButton = new Button
            {
                Content = "Leave Match",
                Width = 150,
                Height = 40,
                Margin = new Thickness(0, 10, 0, 0),
                Style = (Style)FindResource("SecondaryButtonStyle")
            };
            exitButton.Click += (s, e) =>
            {
                MessageBox.Show("Leaving match...");
                CloseSettingsModal(overlay);
            };

            var closeButton = new Button
            {
                Content = "Close",
                Width = 150,
                Height = 40,
                Margin = new Thickness(0, 10, 0, 0),
                Style = (Style)FindResource("PrimaryButtonStyle")
            };
            closeButton.Click += (s, e) => CloseSettingsModal(overlay);

            stack.Children.Add(title);
            stack.Children.Add(volumeLabel);
            stack.Children.Add(volumeSlider);
            stack.Children.Add(exitButton);
            stack.Children.Add(closeButton);

            border.Child = stack;
            overlay.Children.Add(border);

            if (this.Content is Grid grid)
            {
                grid.Children.Add(overlay);
                FadeIn(border);
            }
        }

        private void CloseSettingsModal(UIElement overlay)
        {
            FadeOut(overlay);
            _isSettingsVisible = false;
        }
    }
}
