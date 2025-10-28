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
    /// <summary>
    /// Interaction logic MatchLobbyPage.xaml
    /// </summary>
    public partial class MatchLobbyPage : Page
    {
        private bool _isChatVisible = false;
        private readonly string _currentUserNickname;
        

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

        private void ClickInviteButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            if (sender is Button button && button.DataContext is Friend friend)
            {
                friend.Invited = !friend.Invited;
                button.Content = friend.Invited ? "Invited" : "Invite";
                button.Background = friend.Invited ? Brushes.Green : Brushes.Transparent;
            }
        }

        private void ClickSendInvitesButton(object sender, RoutedEventArgs e)
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

        private void ClickSettingsButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            AnimationUtils.FadeIn(SettingsModal);
        }

        private async void CloseRequestedSettingsModal(object sender, EventArgs e)
        {
            await AnimationUtils.FadeOut(SettingsModal);
        }

        private async void ClickChatButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            if (_isChatVisible)
            {
                await AnimationUtils.FadeOut(ChatControl); 
            }
            else
            {
                AnimationUtils.FadeIn(ChatControl);
            }
            _isChatVisible = !_isChatVisible;
        }

        private async void LeaveMatchRequestedSettingsModal(object sender, EventArgs e)
        {
            try
            {
                ChatControl.Cleanup();

                var mainWindow = Application.Current.MainWindow as UnoLisClient.UI.MainWindow;
                if (mainWindow != null)
                {
                    await mainWindow.RestoreDefaultBackground();
                }
                await AnimationUtils.FadeOut(SettingsModal);
                await AnimationUtils.FadeOutTransition(this.Content as Grid, 0.8);
                NavigationService?.Navigate(new UnoLisClient.UI.Pages.MainMenuPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al salir de la partida: {ex.Message}", "Error al Salir");
            }
        }
    }
}