using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Chat;

namespace UnoLisClient.Logic.Services
{
    public interface IChatService
    {
        event Action<ChatMessageData> OnMessageReceived;
        event Action<ChatMessageData[]> OnChatHistoryReceived;
        event Action<string> OnPlayerDisconnected;
        event Action OnSessionExpired;

        void Initialize(string nickname);
        void Cleanup();

        Task JoinChannelAsync(string channelId);
        Task LeaveChannelAsync(string channelId); 

        Task SendMessageAsync(ChatMessageData message);
    }
}
