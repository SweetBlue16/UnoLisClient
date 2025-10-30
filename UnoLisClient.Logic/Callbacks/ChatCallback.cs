using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.Chat;

namespace UnoLisClient.Logic.Callbacks
{
    public class ChatCallback : IChatManagerCallback
    {
        private readonly ObservableCollection<ChatMessageData> _messageCollection;

        public ChatCallback(ObservableCollection<ChatMessageData> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        public void MessageReceived(ChatMessageData message)
        {
            _messageCollection.Add(message);
        }

        public void ChatHistoryReceived(ChatMessageData[] messages)
        {
            _messageCollection.Clear();
            if (messages == null)
            {
                return;
            }

            foreach (var message in messages)
            {
                _messageCollection.Add(message);
            }
        }

        public void SessionExpired()
        {
            MessageBox.Show("Tu sesión ha expirado. Serás desconectado.", "Sesión Terminada");
        }

        public void PlayerDisconnected(string nickname)
        {
            _messageCollection.Add(new ChatMessageData
            {
                Nickname = "System",
                Message = $"{nickname} se ha desconectado.",
                Timestamp = System.DateTime.Now
            });
        }
    }
}
