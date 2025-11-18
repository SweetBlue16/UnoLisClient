using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows; 
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Friends;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using System.ServiceModel;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private readonly IFriendsService _friendsService;

        public ObservableCollection<Friend> Friends { get; } = new ObservableCollection<Friend>();

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand RefreshFriendsCommand { get; }
        public ICommand RemoveFriendCommand { get; }
        public ICommand OpenAddFriendModalCommand { get; }

        public FriendsViewModel(IFriendsService friendsService, IDialogService dialogService) 
            : base(dialogService)
        {
            _friendsService = friendsService;

            _friendsService.Callback.FriendListUpdatedEvent += OnFriendListUpdated;

            RefreshFriendsCommand = new RelayCommand(async () => await ExecuteRefreshFriendsAsync());
            RemoveFriendCommand = new RelayCommandGeneric<Friend>(async (f) => await ExecuteRemoveFriendAsync(f));
            OpenAddFriendModalCommand = new RelayCommand(ExecuteOpenAddFriendModal);

            _ = ExecuteRefreshFriendsAsync(); 
        }

        private async Task ExecuteRefreshFriendsAsync()
        {
            IsLoading = true;
            try
            {
                var resultList = await _friendsService.GetFriendsListAsync(CurrentSession.CurrentUserNickname);

                await App.Current.Dispatcher.InvokeAsync(() =>
                {
                    Friends.Clear();
                    foreach (var f in resultList.OrderBy(x => x.Nickname))
                        Friends.Add(f);
                });
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Timeout refreshing friends: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Communication error refreshing friends: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error refreshing friends: {ex.Message}";
                HandleException(MessageCode.FriendsInternalError, logMessage, ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ExecuteRemoveFriendAsync(Friend friend)
        {
            if (friend == null) return;

            bool confirmed = _dialogService.ShowQuestionDialog(
                "Confirm Removal",
                $"Are you sure you want to remove {friend.Nickname}?");

            if (!confirmed) return;

            IsLoading = true;
            try
            {
                var request = new FriendRequestData
                {
                    RequesterNickname = CurrentSession.CurrentUserNickname,
                    TargetNickname = friend.Nickname
                };

                bool success = await _friendsService.RemoveFriendAsync(request);

                if (success)
                {
                    await App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Friends.Remove(friend);
                    });

                    _dialogService.ShowAlert("Success", $"{friend.Nickname} was successfully removed.");
                }
                else
                {
                    _dialogService.ShowAlert("Error", $"Could not remove {friend.Nickname}. Please try again.");
                }
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Timeout removing friend: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Communication error removing friend: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error removing friend: {ex.Message}";
                HandleException(MessageCode.FriendsInternalError, logMessage, ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteOpenAddFriendModal()
        {
            string targetNickname = _dialogService.ShowInputDialog(
                "Add a Friend",
                "Enter the nickname of the player you wish to add:", "Nickname...");

            if (!string.IsNullOrWhiteSpace(targetNickname))
            {
                _ = ExecuteSendFriendRequestAsync(targetNickname); 
            }
        }

        private async Task ExecuteSendFriendRequestAsync(string targetNickname)
        {
            IsLoading = true;
            try
            {
                if (string.Equals(targetNickname, CurrentSession.CurrentUserNickname, StringComparison.OrdinalIgnoreCase))
                {
                    _dialogService.ShowWarning(MessageTranslator.GetMessage(MessageCode.CannotAddSelfAsFriend));
                    return;
                }

                var requestResult = await _friendsService.SendFriendRequestAsync(
                    CurrentSession.CurrentUserNickname,
                    targetNickname);

                HandleFriendRequestResult(requestResult, targetNickname);
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Timeout sending friend request: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Communication error sending friend request: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Fatal error sending friend request: {ex.Message}";
                HandleException(MessageCode.FriendsInternalError, logMessage, ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void HandleFriendRequestResult(FriendRequestResult result, string targetNickname)
        {
            MessageCode code;

            switch (result)
            {
                case FriendRequestResult.Success:
                    code = MessageCode.FriendRequestSent;
                    break;
                case FriendRequestResult.UserNotFound:
                    code = MessageCode.PlayerNotFound;
                    break;
                case FriendRequestResult.AlreadyFriends:
                    code = MessageCode.AlreadyFriends;
                    break;
                case FriendRequestResult.RequestAlreadySent:
                    code = MessageCode.PendingFriendRequest;
                    break;
                case FriendRequestResult.RequestAlreadyReceived:
                    code = MessageCode.FriendRequestReceived;
                    break;
                case FriendRequestResult.CannotAddSelf:
                    code = MessageCode.CannotAddSelfAsFriend;
                    break;
                default:
                    code = MessageCode.GeneralServerError;
                    break;
            }

            string message = MessageTranslator.GetMessage(code);

            try
            {
                message = string.Format(message, targetNickname);
            }
            catch (FormatException) //¿Esta excepción puede quedarse sin nada dentro, sugerencia de la IA?
            {
                LogManager.Error("String format error in HandleFriendRequestResult message formatting.");
            }

            if (result == FriendRequestResult.Success)
            {
                _dialogService.ShowAlert("Friend Request", message);
            }
            else
            {
                _dialogService.ShowWarning(message);
            }
        }

        private void OnFriendListUpdated(List<FriendData> updatedList)
        {
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                Friends.Clear();
                var localFriends = updatedList.Select(dto => new Friend(dto));

                foreach (var f in localFriends.OrderBy(x => x.Nickname))
                {
                    Friends.Add(f);
                }
            });
        }

    }
}