using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Chat;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisServer.Common.Enums;
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly IChatService _chatService;
        private readonly string _channelId;
        private readonly string _currentUserNickname;

        public ObservableCollection<ChatMessageData> Messages { get; } = new ObservableCollection<ChatMessageData>();

        private string _currentMessage;
        public string CurrentMessage
        {
            get => _currentMessage;
            set => SetProperty(ref _currentMessage, value);
        }

        public ICommand SendMessageCommand { get; }

        /// <summary>
        /// Creates a new instance of ChatViewModel for a specific chat channel.
        /// </summary>
        public ChatViewModel(IChatService chatService, IDialogService dialogService, string channelId, string nickname)
            : base(dialogService)
        {
            _chatService = chatService;
            _channelId = channelId;
            _currentUserNickname = nickname;

            SendMessageCommand = new RelayCommand(async () => await ExecuteSendMessageAsync(), () => 
            !string.IsNullOrWhiteSpace(CurrentMessage) && !IsLoading);
        }

        public async Task InitializeAsync()
        {
            _chatService.OnMessageReceived += HandleMessageReceived;
            _chatService.OnChatHistoryReceived += HandleChatHistoryReceived;
            _chatService.OnPlayerDisconnected += HandlePlayerDisconnected;
            _chatService.OnSessionExpired += HandleSessionExpired;

            Messages.Clear();
            Messages.Add(new ChatMessageData { Nickname = Global.SystemLabel, Message = string.Format(
                Lobby.ConnectingChannelMessageLabel, _channelId) });

            try
            {
                _chatService.Initialize(_currentUserNickname);
                await _chatService.JoinChannelAsync(_channelId);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ChatInternalError, $"Error al unirse al canal de chat: {ex.Message}", ex);
            }
        }

        public Task CleanupAsync()
        {
            _chatService.OnMessageReceived -= HandleMessageReceived;
            _chatService.OnChatHistoryReceived -= HandleChatHistoryReceived;
            _chatService.OnPlayerDisconnected -= HandlePlayerDisconnected;
            _chatService.OnSessionExpired -= HandleSessionExpired;

            return Task.CompletedTask;
        }

        private async Task ExecuteSendMessageAsync()
        {
            var message = new ChatMessageData
            {
                Nickname = _currentUserNickname,
                Message = this.CurrentMessage,
                ChannelId = _channelId,
                Timestamp = DateTime.Now
            };

            try
            {
                await _chatService.SendMessageAsync(message);
                CurrentMessage = string.Empty;
            }
            catch (CommunicationException ex)
            {
                HandleException(MessageCode.ConnectionFailed, $"Error de red al enviar mensaje: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ChatInternalError, $"Error al enviar mensaje: {ex.Message}", ex);
            }
        }
        
        private void HandleMessageReceived(ChatMessageData message)
        {
            if (message.ChannelId != _channelId) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(message);
            });
        }

        private void HandleChatHistoryReceived(ChatMessageData[] messages)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var message in messages)
                {
                    if (message.ChannelId == _channelId)
                    {
                        Messages.Add(message);
                    }
                }
            });
        }
        
        private void HandlePlayerDisconnected(string nickname)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new ChatMessageData 
                { 
                    Nickname = Global.SystemLabel, 
                    Message = string.Format(Global.PlayerDisconnectedMessageLabel, nickname),
                    ChannelId = _channelId
                });
            });
        }

        private void HandleSessionExpired()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentMessage = Lobby.CantSendMoreMessagesLabel;
                IsLoading = true;
            });
        }
    }
}
