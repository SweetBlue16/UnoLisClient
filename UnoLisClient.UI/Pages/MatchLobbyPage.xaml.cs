using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UnoLisClient.UI.Pages
{
    public class Friend
    {
        public string FriendName { get; set; }
        public string Status { get; set; }
        public Brush StatusColor { get; set; }
        public bool Invited { get; set; }
    }

    public partial class MatchLobbyPage : Page
    {
        public ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        private bool _isChatVisible = false;
        private Grid _settingsOverlay; // referencia al overlay de ajustes

        public MatchLobbyPage()
        {
            InitializeComponent();
            LoadFriends();

            // 🎬 Cambiar fondo y música al entrar al lobby con transición
            var mainWindow = Application.Current.MainWindow as UnoLisClient.UI.MainWindow;
            mainWindow?.SetBackgroundMedia("Assets/lobbyVideo.mp4", "Assets/lobbyMusic.mp3");
        }

        // 🔹 Cargar lista de amigos simulada
        private void LoadFriends()
        {
            Friends.Add(new Friend { FriendName = "SweetBlue16", Status = "Online", StatusColor = Brushes.Lime });
            Friends.Add(new Friend { FriendName = "MapleVR", Status = "Offline", StatusColor = Brushes.Gray });
            Friends.Add(new Friend { FriendName = "Erickmel", Status = "Online", StatusColor = Brushes.Lime });
            Friends.Add(new Friend { FriendName = "IngeAbraham", Status = "Online", StatusColor = Brushes.Lime });
            FriendsList.ItemsSource = Friends;
        }

        // 💌 Invitar amigos individuales
        private void InviteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Friend friend)
            {
                friend.Invited = !friend.Invited;
                button.Content = friend.Invited ? "Invited" : "Invite";
                button.Background = friend.Invited ? Brushes.Green : Brushes.Transparent;
            }
        }

        // ✉️ Enviar invitaciones seleccionadas
        private void SendInvitesButton_Click(object sender, RoutedEventArgs e)
        {
            var invited = Friends.Where(f => f.Invited).Select(f => f.FriendName).ToList();

            if (invited.Any())
            {
                MessageBox.Show($"Invitations sent to: {string.Join(", ", invited)}", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No friends selected for invitation.", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            if (_settingsOverlay != null)
                return;

            ShowSettingsModal();
        }

        // 🟢 Animación Fade In
        private void FadeIn(UIElement element, double duration = 0.3)
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

        // 🔴 Animación Fade Out
        private async void FadeOut(UIElement element, double duration = 0.25)
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

        // ⚙️ Modal de Ajustes
        private void ShowSettingsModal()
        {
            _settingsOverlay = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0)),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(230, 30, 30, 30)),
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
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var volumeLabel = new TextBlock
            {
                Text = "Volume",
                FontSize = 16,
                Foreground = Brushes.White
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
                CloseSettingsModal();
            };

            var closeButton = new Button
            {
                Content = "Close",
                Width = 150,
                Height = 40,
                Margin = new Thickness(0, 10, 0, 0),
                Style = (Style)FindResource("PrimaryButtonStyle")
            };
            closeButton.Click += (s, e) => CloseSettingsModal();

            stack.Children.Add(title);
            stack.Children.Add(volumeLabel);
            stack.Children.Add(volumeSlider);
            stack.Children.Add(exitButton);
            stack.Children.Add(closeButton);

            border.Child = stack;
            _settingsOverlay.Children.Add(border);

            var root = Window.GetWindow(this)?.Content as Grid;
            if (root != null)
                root.Children.Add(_settingsOverlay);
            else if (this.Content is Grid grid)
                grid.Children.Add(_settingsOverlay);

            FadeIn(_settingsOverlay, 0.3);
        }

        // 🔧 Cerrar modal
        private async void CloseSettingsModal()
        {
            if (_settingsOverlay == null)
                return;

            FadeOut(_settingsOverlay, 0.25);
            await Task.Delay(250);

            if (this.Content is Grid grid && grid.Children.Contains(_settingsOverlay))
                grid.Children.Remove(_settingsOverlay);

            _settingsOverlay = null;
        }
    }
}
