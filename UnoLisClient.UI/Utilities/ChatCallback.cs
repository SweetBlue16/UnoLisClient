using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnoLisClient.UI.UnoLisServerReference.Chat;

namespace UnoLisClient.UI.Utilities
{
    public class ChatCallback : IChatManagerCallback
    {
        private readonly ObservableCollection<ChatMessageData> _messageCollection;

        public ChatCallback(ObservableCollection<ChatMessageData> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        #region Implementación de IChatCallback

        public void MessageReceived(ChatMessageData message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _messageCollection.Add(message);
            });
        }

        public void ChatHistoryReceived(ChatMessageData[] messages)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _messageCollection.Clear();
                if (messages == null) return;

                foreach (var message in messages)
                {
                    _messageCollection.Add(message);
                }
            });
        }

        #endregion

        #region Implementación de ISessionCallback (Heredada)
        public void SessionExpired()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Tu sesión ha expirado. Serás desconectado.", "Sesión Terminada");
            });
        }

        public void PlayerDisconnected(string nickname)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _messageCollection.Add(new ChatMessageData
                {
                    Nickname = "System",
                    Message = $"{nickname} se ha desconectado.",
                    Timestamp = System.DateTime.Now
                });
            });
        }

        #endregion
    }
}
