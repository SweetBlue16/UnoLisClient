using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.Properties.Langs;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.Utilities
{
    public static class MessageTranslator
    {
        private static readonly Dictionary<MessageCode, string> _messageMap;

        static MessageTranslator()
        {
            _messageMap = new Dictionary<MessageCode, string>
            {
                { MessageCode.Success, Global.SuccessfulOperationMessageLabel },
                { MessageCode.RegistrationSuccessful, SignIn.UserRegisteredSuccessfullyLabel },
                { MessageCode.LoginSuccessful, SignIn.UserLoggedInSuccessfullyLabel },
                { MessageCode.LogoutSuccessful, SignIn.UserLoggedOutSuccessfullyLabel },
                { MessageCode.ProfileDataRetrieved, Profile.UserDataObtainedLabel },
                { MessageCode.ProfileUpdated, Profile.UserDataChangedLabel },
                { MessageCode.FriendRequestSent, FriendsList.FriendRequestSentLabel },
                { MessageCode.FriendRequestAccepted, FriendsList.FriendRequestAcceptedLabel },
                { MessageCode.FriendRemoved, FriendsList.FriendRemovedMessageLabel },
                { MessageCode.ChatMessageSent, Lobby.NewMessageSentLabel },
                { MessageCode.ChatMessageRetrieved, Lobby.MessageRetrievedLabel },
                { MessageCode.LobbyCreated, Lobby.LobbyCreatedSuccessfullyLabel },
                { MessageCode.LobbyJoined, Lobby.PlayerJoinedLabel },
                { MessageCode.LobbyLeft, Lobby.PlayerLeftLabel },
                { MessageCode.MatchStarted, Match.MatchStartedLabel },
                { MessageCode.MatchEnded, Match.MatchOverLabel },
                { MessageCode.PlayerReady, Lobby.PlayerReadyLabel },
                { MessageCode.PlayerNotReady, Lobby.PlayerNotReadyLabel },
                { MessageCode.RewardGranted, Shop.RewardObtainedLabel },
                { MessageCode.PurchaseCompleted, Shop.PurchaseSuccessfulLabel }
            };
        }

        public static string GetMessage(MessageCode code)
        {
            if (_messageMap.TryGetValue(code, out string message))
            {
                return message;
            }
            return ErrorMessages.UnknownErrorMessageLabel;
        }
    }
}
