using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Chat;

namespace UnoLisClient.Logic.Callbacks
{
    public class ChatCallback : IChatManagerCallback
    {
        public static event Action<ChatMessageData> OnMessageReceived;
        public static event Action<ChatMessageData[]> OnChatHistoryReceived;
        public static event Action<string> OnPlayerDisconnected;
        public static event Action OnSessionExpired;

        public void MessageReceived(ChatMessageData message)
        {
            OnMessageReceived?.Invoke(message);
        }

        public void ChatHistoryReceived(ChatMessageData[] messages)
        {
            OnChatHistoryReceived?.Invoke(messages);
        }

        public void PlayerDisconnected(string nickname)
        {
            OnPlayerDisconnected?.Invoke(nickname);
        }

        public void SessionExpired()
        {
            OnSessionExpired?.Invoke();
            MessageBox.Show("Tu sesión ha expirado. Serás desconectado.", "Sesión Terminada");
        }
    }
}
