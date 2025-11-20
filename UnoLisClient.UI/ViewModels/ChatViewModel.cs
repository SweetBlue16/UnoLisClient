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
        /// <param name="chatService">El servicio de chat (Singleton).</param>
        /// <param name="dialogService">El servicio de diálogos (heredado).</param>
        /// <param name="channelId">El ID de la sala a la que se unirá (ej. "LOBBY_123" o "MATCH_ABC").</param>
        /// <param name="nickname">El nickname del usuario actual.</param>
        public ChatViewModel(IChatService chatService, IDialogService dialogService, string channelId, string nickname)
            : base(dialogService)
        {
            _chatService = chatService;
            _channelId = channelId;
            _currentUserNickname = nickname;

            SendMessageCommand = new RelayCommand(async () => await ExecuteSendMessageAsync(), () => !string.IsNullOrWhiteSpace(CurrentMessage) && !IsLoading);
        }

        public async Task InitializeAsync()
        {
            _chatService.OnMessageReceived += HandleMessageReceived;
            _chatService.OnChatHistoryReceived += HandleChatHistoryReceived;
            _chatService.OnPlayerDisconnected += HandlePlayerDisconnected;
            _chatService.OnSessionExpired += HandleSessionExpired;

            Messages.Clear();
            Messages.Add(new ChatMessageData { Nickname = "System", Message = $"Conectado al canal '{_channelId}'." });

            try
            {
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
            
            // (Opcional: llamar a _chatService.LeaveChannelAsync(_channelId) si el servidor lo implementa)
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
                // TODO Add auto-scroll to bottom
            });
        }

        private void HandleChatHistoryReceived(ChatMessageData[] messages)
        {
            // (Filtro futuro: comprobar si el historial es para este channelId)
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Messages.Clear(); // (Opcional, depende de si quieres borrar el "Conectado a...")
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
                    Nickname = "System", 
                    Message = $"{nickname} se ha desconectado.",
                    ChannelId = _channelId
                });
            });
        }

        private void HandleSessionExpired()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentMessage = "Tu sesión ha expirado. No puedes enviar más mensajes.";
                IsLoading = true;
            });
        }
    }
}
