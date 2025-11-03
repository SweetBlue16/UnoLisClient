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
using System.Collections.ObjectModel;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.Chat;
using UnoLisClient.UI.Utilities;
using UnoLisClient.Logic.Callbacks;

namespace UnoLisClient.UI.Views.Controls
{
    public partial class ChatControl : UserControl
    {
        private const string GlobalLobbyChannel = "GlobalLobby";

        private ChatManagerClient _chatClient;
        private ChatCallback _chatCallback;
        private string _currentUserNickname;

        public ObservableCollection<ChatMessageData> ChatMessages { get; set; } = new ObservableCollection<ChatMessageData>();

        public ChatControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void Initialize(string userNickname)
        {
            _currentUserNickname = userNickname;
            InitializeChatService();
        }

        public void Cleanup()
        {
            CleanupChatService();
        }

        private void InitializeChatService()
        {
            try
            {
                _chatCallback = new ChatCallback(this.ChatMessages);
                InstanceContext context = new InstanceContext(_chatCallback);
                _chatClient = new ChatManagerClient(context);
                _chatClient.Open();

                _chatClient.RegisterPlayer(_currentUserNickname);
                _chatClient.GetChatHistory(GlobalLobbyChannel);

                ChatMessages.Add(new ChatMessageData
                {
                    Nickname = "System",
                    Message = $"Conectado al chat como '{_currentUserNickname}'."
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar al chat: {ex.Message}");
                if (_chatClient != null) _chatClient.Abort();
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string messageText = ChatInput.Text;
            if (string.IsNullOrWhiteSpace(messageText) || _chatClient == null) return;

            try
            {
                var messageData = new ChatMessageData
                {
                    Nickname = _currentUserNickname,
                    Message = messageText,
                    ChannelId = GlobalLobbyChannel,
                    Timestamp = DateTime.Now
                };

                _chatClient.SendMessage(messageData);
                ChatInput.Text = string.Empty;
                ChatInput.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar mensaje: {ex.Message}");
            }
        }

        private void CleanupChatService()
        {
            if (_chatClient == null) return;
            try
            {
                if (_chatClient.State == CommunicationState.Opened) _chatClient.Close();
                else _chatClient.Abort();
            }
            catch (Exception)
            {
                _chatClient.Abort();
            }
            finally
            {
                _chatClient = null;
            }
        }
    }
}