using System;
using System.Collections.Generic;
using UnoLisClient.Logic.UnoLisServerReference.Friends;
using System.Linq;

namespace UnoLisClient.Logic.Callbacks
{
    /// <summary>
    /// Callback del cliente para manejar eventos enviados por el servidor FriendsManager.
    /// </summary>
    public class FriendsCallback : IFriendsManagerCallback
    {
        public event Action<List<FriendData>> FriendsListReceivedEvent;
        public event Action<List<FriendRequestData>> PendingRequestsReceivedEvent;
        public event Action<FriendRequestData> FriendRequestReceivedEvent;
        public event Action<List<FriendData>> FriendListUpdatedEvent;

        public event Action<string, bool> FriendActionNotificationEvent;

        public void FriendActionNotification(string message, bool isSuccess)
        {
            FriendActionNotificationEvent?.Invoke(message, isSuccess);
        }

        public void FriendsListReceived(FriendData[] friends)
        {
            FriendsListReceivedEvent?.Invoke(friends.ToList());
        }

        public void PendingRequestsReceived(FriendRequestData[] requests)
        {
            PendingRequestsReceivedEvent?.Invoke(requests.ToList());
        }

        public void FriendRequestReceived(FriendRequestData newRequest)
        {
            FriendRequestReceivedEvent?.Invoke(newRequest);
        }
        public void FriendListUpdated(FriendData[] updatedList)
        {
            FriendListUpdatedEvent?.Invoke(updatedList.ToList());
        }
    }
}