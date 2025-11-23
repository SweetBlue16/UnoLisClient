using System.Collections.Generic;
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
                { MessageCode.PurchaseCompleted, Shop.PurchaseSuccessfulLabel },
                { MessageCode.VerificationCodeSent, SignIn.VerificationCodeSentLabel },
                { MessageCode.VerificationCodeResent, SignIn.ResendVerificationCodeLabel },
                { MessageCode.FriendRequestReceived, FriendsList.FriendRequestReceivedMessageLabel },
                { MessageCode.FriendRequestRecalled, FriendsList.FriendRequestRecalledMessageLabel },
                { MessageCode.FriendRequestDeclined, FriendsList.FriendRequestDeclinedMessageLabel },
                { MessageCode.LeaderboardDataRetrieved, Global.LeaderboardDataReceivedMessageLabel },
                { MessageCode.SettingUpdated, Settings.UserSettingsUpdatedMessageLabel },
                { MessageCode.AvatarChanged, Profile.AvatarChangedSuccessfulMessageLabel },
                { MessageCode.LobbySettingsUpdated, Lobby.PlayersListUpdated },
                { MessageCode.MatchPlayerTurn, Match.TurnLabel },
                { MessageCode.MatchCardDrawn, Match.CardDrawnMessageLabel },
                { MessageCode.MatchUnoDeclared, Match.PlayerDeclaredUnoMessageLabel },
                { MessageCode.MatchWinnerDeclared, Match.WinnerLabel },
                { MessageCode.MatchScoreUpdated, Match.MatchResultsRecordedMessageLabel },
                { MessageCode.SanctionApplied, Global.SanctionAppliedMessageLabel },
                { MessageCode.ReportSubmitted, Global.ReportSubmittedMessageLabel },

                { MessageCode.BadRequest, ErrorMessages.BadRequestMessageLabel },
                { MessageCode.InvalidData, ErrorMessages.InvalidDataMessageLabel },
                { MessageCode.InvalidCredentials, ErrorMessages.InvalidCredentialsMessageLabel },
                { MessageCode.NicknameAlreadyTaken, ErrorMessages.NicknameAlreadyTakenMessageLabel },
                { MessageCode.EmailAlreadyRegistered, ErrorMessages.EmailAlreadyRegisteredMessageLabel },
                { MessageCode.InvalidEmailFormat, ErrorMessages.EmailFormatMessageLabel },
                { MessageCode.WeakPassword, ErrorMessages.WeakPasswordMessageLabel },
                { MessageCode.PlayerNotFound, ErrorMessages.PlayerNotFoundMessageLabel },
                { MessageCode.SamePassword, ErrorMessages.PasswordSameAsOldMessageLabel },
                { MessageCode.EmptyMessage, ErrorMessages.MessageCantBeEmptyMessageLabel },
                { MessageCode.MessageTooLong, ErrorMessages.MessageLengthMessageLabel },
                { MessageCode.InappropriateContent, ErrorMessages.InappropriatedContentMessageLabel },
                { MessageCode.AlreadyFriends, ErrorMessages.AlreadyFriendsMessageLabel },
                { MessageCode.PendingFriendRequest, ErrorMessages.PendingFriendRequestMessageLabel },
                { MessageCode.InvalidSocialUrl, ErrorMessages.SocialLinkFormatMessageLabel },
                { MessageCode.BlockedUser, ErrorMessages.BlockedUserMessageLabel },
                { MessageCode.LobbyNotFound, ErrorMessages.LobbyNotFoundMessageLabel },
                { MessageCode.LobbyFull, ErrorMessages.LobbyFullMessageLabel },
                { MessageCode.OperationNotSupported, ErrorMessages.OperationNotSupportedMessageLabel },
                { MessageCode.ValidationFailed, ErrorMessages.ValidationFailedMessageLabel },
                { MessageCode.EmptyFields, ErrorMessages.EmptyFieldsMessageLabel },
                { MessageCode.RateLimitExceeded, ErrorMessages.RateLimitExceededMessageLabel },
                { MessageCode.RegistrationDataLost, ErrorMessages.RegistrationDataLostMessageLabel },
                { MessageCode.VerificationCodeInvalid, ErrorMessages.VerificationCodeInvalidMessageLabel },
                { MessageCode.InvalidUrlFormat, ErrorMessages.SocialLinkFormatMessageLabel },
                { MessageCode.PasswordDontMatch, ErrorMessages.PasswordDontMatchMessageLabel },
                { MessageCode.FriendRequestNotFound, ErrorMessages.FriendRequestNotFoundMessageLabel },
                { MessageCode.CannotAddSelfAsFriend, ErrorMessages.CannotAddSelfMessageLabel },
                { MessageCode.InvalidAvatarSelection, ErrorMessages.InvalidAvatarSelectionMessageLabel },
                { MessageCode.NotEnoughCurrency, ErrorMessages.NotEnoughCurrencyMessageLabel },
                { MessageCode.ItemNotInShop, ErrorMessages.ItemNotInShopMessageLabel },
                { MessageCode.PlayerReported, Match.PlayerReportedMessageLabel },
                { MessageCode.InvalidSearchQuery, ErrorMessages.InvalidSearchQueryMessageLabel },
                { MessageCode.AlreadyReportedRecently, ErrorMessages.AlreadyReportedRecentlyMessageLabel },

                { MessageCode.SessionExpired, ErrorMessages.SessionExpiredMessageLabel },
                { MessageCode.UnauthorizedAccess, ErrorMessages.UnauthorizedAccessMessageLabel },
                { MessageCode.InvalidToken, ErrorMessages.InvalidTokenMessageLabel },
                { MessageCode.MissingToken, ErrorMessages.MissingTokenMessageLabel },
                { MessageCode.DuplicateSession, ErrorMessages.DuplicateSessionMessageLabel },
                { MessageCode.UserNotConnected, ErrorMessages.UserNotConnectedMessageLabel },
                { MessageCode.LoginInternalError, ErrorMessages.LoginInternalErrorMessageLabel },
                { MessageCode.LogoutInternalError, ErrorMessages.LogoutInternalErrorMessageLabel },
                { MessageCode.AccountNotVerified, ErrorMessages.AccountNotVerifiedMessageLabel },

                { MessageCode.DatabaseError, ErrorMessages.DatabaseErrorMessageLabel },
                { MessageCode.TransactionFailed, ErrorMessages.TransactionFailedMessageLabel },
                { MessageCode.SqlError, ErrorMessages.SqlErrorMessageLabel },
                { MessageCode.ConcurrencyConflict, ErrorMessages.ConcurrencyConflictMessageLabel },
                { MessageCode.SerializationError, ErrorMessages.SerializationErrorMessageLabel },
                { MessageCode.UnhandledException, ErrorMessages.UnhandledExceptionMessageLabel },
                { MessageCode.CallbackError, ErrorMessages.CallbackErrorMessageLabel },
                { MessageCode.ProfileUpdateFailed, ErrorMessages.ProfileUpdateFailedMessageLabel },
                { MessageCode.ProfileFetchFailed, ErrorMessages.ProfileFetchFailedMessageLabel },
                { MessageCode.ChatInternalError, ErrorMessages.ChatInternalErrorMessageLabel },
                { MessageCode.FriendsInternalError, ErrorMessages.FriendsInternalErrorMessageLabel },
                { MessageCode.LobbyInternalError, ErrorMessages.LobbyInternalErrorMessageLabel },
                { MessageCode.GeneralServerError, ErrorMessages.GeneralServerErrorMessageLabel },
                { MessageCode.RegistrationInternalError, ErrorMessages.RegistrationInternalErrorMessageLabel },
                { MessageCode.ConfirmationInternalError, ErrorMessages.ConfirmationInternalErrorMessageLabel },
                { MessageCode.EmailSendingFailed, ErrorMessages.EmailSendingFailedMessageLabel },
                { MessageCode.FileSystemError, ErrorMessages.FileSystemErrorMessageLabel },
                { MessageCode.ServiceInitializationFailed, ErrorMessages.ServiceInitializationFailedMessageLabel },
                { MessageCode.ShopInternalError, ErrorMessages.ShopInternalErrorMessageLabel },
                { MessageCode.ReportInternalError, ErrorMessages.ReportInternalErrorMessageLabel },

                { MessageCode.ConnectionLost, ErrorMessages.NoConnectionMessageLabel },
                { MessageCode.Timeout, ErrorMessages.TimeoutMessageLabel },
                { MessageCode.ConnectionFailed, ErrorMessages.ConnectionErrorMessageLabel },
                { MessageCode.ConnectionRejected, ErrorMessages.ConnectionRejectedMessageLabel },
                { MessageCode.UnstableConnection, ErrorMessages.UnstableConnectionMessageLabel },
                { MessageCode.ClientDisconnected, ErrorMessages.ClientDisconnectedMessageLabel },

                { MessageCode.PlayerBlocked, FriendsList.PlayerBlockedMessageLabel },
                { MessageCode.PlayerUnblocked, FriendsList.PlayerUnblockedMessageLabel },
                { MessageCode.PlayerHasActiveLobby, ErrorMessages.PlayerHasActiveLobbyMessageLabel },
                { MessageCode.PlayerNotInLobby, ErrorMessages.PlayerNotInLobbyMessageLabel },
                { MessageCode.PlayerAlreadyReady, ErrorMessages.PlayerAlreadyReadyMessageLabel },
                { MessageCode.PlayerWereNotReady, ErrorMessages.PlayerWereNotReadyMessageLabel },
                { MessageCode.MatchAlreadyStarted, ErrorMessages.MatchAlreadyStartedMessageLabel },
                { MessageCode.MatchCancelled, ErrorMessages.MatchCancelledMessageLabel },
                { MessageCode.MatchNotFound, ErrorMessages.MatchNotFoundMessageLabel },
                { MessageCode.MatchAlreadyEnded, ErrorMessages.MatchAlreadyEndedMessageLabel },
                { MessageCode.PlayerKicked, Match.PlayerKickedMessageLabel },
                { MessageCode.PlayerBanned, Match.PlayerBannedMessageLabel },
                { MessageCode.LobbyClosed, ErrorMessages.LobbyClosedMessageLabel },
                { MessageCode.NoPermissions, ErrorMessages.NoPermissionsMessageLabel },
                { MessageCode.LobbyInconsistentState, ErrorMessages.LobbyInconsistentStateMessageLabel },
                { MessageCode.PlayerDisconnected, ErrorMessages.PlayerDisconnectedMessageLabel },
                { MessageCode.PlayerReconnected, Global.PlayerReconnectedMessageLabel },
                { MessageCode.MatchResultsRecorded, Match.MatchResultsRecordedMessageLabel },
                { MessageCode.RewardProcessingError, ErrorMessages.RewardProcessingErrorMessageLabel },
                { MessageCode.PurchaseProcessingError, ErrorMessages.PurchaseProcessingErrorMessageLabel },
                { MessageCode.InvalidMove, Match.InvalidMoveMessageLabel },
                { MessageCode.NotPlayerTurn, ErrorMessages.NotPlayerTurnMessageLabel },
                { MessageCode.MustDrawCards, ErrorMessages.MustDrawCardsMessageLabel },
                { MessageCode.MustPlayOrDraw, ErrorMessages.MustPlayOrDrawMessageLabel },
                { MessageCode.ForgotToCallUno, ErrorMessages.ForgotToCallUnoMessageLabel },
                { MessageCode.DeckIsEmpty, ErrorMessages.DeckIsEmptyMessageLabel },
                { MessageCode.PlayerLimitReached, ErrorMessages.PlayerLimitReachedMessageLabel },
                { MessageCode.PlayerReportFailed, ErrorMessages.PlayerReportFailedMessageLabel },
                { MessageCode.ItemAlreadyOwned, Shop.ItemAlreadyOwnedMessageLabel }
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