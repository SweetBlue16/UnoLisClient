using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.UnoLisServerReference.Chat;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Utils;

namespace UnoLisClient.UI.Pages
{
    public partial class MatchLobbyPage : Page
    {
        private const string GlobalLobbyChannel = "GlobalLobby";

        private bool _isChatVisible = false;
        private ChatManagerClient _chatClient;
        private ChatCallback _chatCallback;
        private string _currentUserNickname;
        

        public ObservableCollection<ChatMessageData> ChatMessages { get; set; } = new ObservableCollection<ChatMessageData>();
        public ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();

        public MatchLobbyPage()
        {
            InitializeComponent();
            _currentUserNickname = CurrentSession.CurrentUserNickname;
            LoadFriends();
            ChatControl.Initialize(_currentUserNickname);

            this.DataContext = this;
        }

        private void LoadFriends()
        {
            Friends.Add(new Friend { FriendName = "SweetBlue16", Status = "Online", StatusColor = Brushes.Lime });
            Friends.Add(new Friend { FriendName = "MapleVR", Status = "Offline", StatusColor = Brushes.Gray });
            Friends.Add(new Friend { FriendName = "Erickmel", Status = "Online", StatusColor = Brushes.Lime });
            Friends.Add(new Friend { FriendName = "IngeAbraham", Status = "Online", StatusColor = Brushes.Lime });
            FriendsList.ItemsSource = Friends;
        }

        private void InviteButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            if (sender is Button button && button.DataContext is Friend friend)
            {
                friend.Invited = !friend.Invited;
                button.Content = friend.Invited ? "Invited" : "Invite";
                button.Background = friend.Invited ? Brushes.Green : Brushes.Transparent;
            }
        }

        private void SendInvitesButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
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

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            AnimationUtils.FadeIn(SettingsModal);
        }

        private async void SettingsModal_CloseRequested(object sender, EventArgs e)
        {
            await AnimationUtils.FadeOut(SettingsModal);
        }

        private async void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            // 👇 2. AHORA ANIMAMOS EL CONTROL
            if (_isChatVisible)
            {
                await AnimationUtils.FadeOut(ChatControl); // Animamos el UserControl
            }
            else
            {
                AnimationUtils.FadeIn(ChatControl); // Animamos el UserControl
            }
            _isChatVisible = !_isChatVisible;
        }

        private async void SettingsModal_LeaveMatchRequested(object sender, EventArgs e)
        {
            ChatControl.Cleanup();
            var mainWindow = Application.Current.MainWindow as UnoLisClient.UI.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.RestoreDefaultBackground();
            }

            await AnimationUtils.FadeOut(SettingsModal);

            await AnimationUtils.FadeOutTransition(this.Content as Grid, 0.8);

            NavigationService?.Navigate(new UnoLisClient.UI.Pages.MainMenuPage());
        }
    }
}