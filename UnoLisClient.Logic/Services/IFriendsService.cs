using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.UnoLisServerReference.Friends;

namespace UnoLisClient.Logic.Services
{
    public interface IFriendsService
    {
        void Initialize(string nickname);
        void Cleanup();
        Task<FriendRequestResult> SendFriendRequestAsync(string requesterNickname, string targetNickname);
        Task<List<Friend>> GetFriendsListAsync(string nickname);
        Task<List<FriendRequestData>> GetPendingRequestsAsync(string nickname);
        Task<bool> AcceptFriendRequestAsync(FriendRequestData request);
        Task<bool> RejectFriendRequestAsync(FriendRequestData request);
        Task<bool> RemoveFriendAsync(FriendRequestData request);

        FriendsCallback Callback { get; }
    }
}