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

namespace UnoLisClient.UI.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private readonly IFriendsService _friendsService;
        private readonly IModalService _modalService; 

        public ObservableCollection<Friend> Friends { get; } = new ObservableCollection<Friend>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand RefreshFriendsCommand { get; }
        public ICommand RemoveFriendCommand { get; }
        public ICommand OpenAddFriendModalCommand { get; }

        public FriendsViewModel(IFriendsService friendsService, IModalService modalService)
        {
            _friendsService = friendsService;
            _modalService = modalService;

            _friendsService.Callback.FriendActionNotificationEvent += OnFriendActionNotification;
            _friendsService.Callback.FriendListUpdatedEvent += OnFriendListUpdated;
            _friendsService.Callback.FriendRequestReceivedEvent += OnFriendRequestReceived;

            RefreshFriendsCommand = new RelayCommand(async () => await ExecuteRefreshFriendsAsync());
            RemoveFriendCommand = new RelayCommand<Friend>(async (f) => await ExecuteRemoveFriendAsync(f));
            OpenAddFriendModalCommand = new RelayCommand(ExecuteOpenAddFriendModal);

            _friendsService.Subscribe(CurrentSession.CurrentUserNickname);
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
            catch (Exception ex)
            {
                LogManager.Error($"Error refreshing friends: {ex.Message}", ex);
                _modalService.ShowAlert("Error", "Could not load friends list due to a server error.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ExecuteRemoveFriendAsync(Friend friend)
        {
            if (friend == null) return;

            var confirm = _modalService.ShowConfirmation(
                "Confirm Removal",
                $"Are you sure you want to remove {friend.Nickname}?");

            if (confirm != MessageBoxResult.Yes) return;

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

                    _modalService.ShowAlert("Success", $"{friend.Nickname} was successfully removed.");
                }
                else
                {
                    _modalService.ShowAlert("Error", $"Could not remove {friend.Nickname}. Please try again.");
                }
            }
            catch (Exception ex)
            {
                LogManager.Error($"Error removing friend: {ex.Message}", ex);
                _modalService.ShowAlert("Error", "A fatal error occurred while processing the request.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteOpenAddFriendModal()
        {
            string targetNickname = _modalService.ShowTextInputDialog(
                "Add a Friend",
                "Enter the nickname of the player you wish to add:");

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
                    _modalService.ShowAlert("Warning", "You cannot add yourself.");
                }

                var requestResult = await _friendsService.SendFriendRequestAsync(
                    CurrentSession.CurrentUserNickname,
                    targetNickname);

                HandleFriendRequestResult(requestResult, targetNickname);
            }
            catch (Exception ex)
            {
                LogManager.Error($"Fatal error sending friend request: {ex.Message}", ex);
                _modalService.ShowAlert("Error", "An unexpected error occurred. Please check server status.");
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        private void HandleFriendRequestResult(FriendRequestResult result, string targetNickname)
        {
            string title = "Friend Request";
            string message;

            switch (result)
            {
                case FriendRequestResult.Success:
                    message = $"Friend request sent to {targetNickname}.";
                    break;
                case FriendRequestResult.UserNotFound:
                    message = $"Player '{targetNickname}' was not found.";
                    break;
                case FriendRequestResult.AlreadyFriends:
                    message = $"You are already friends with {targetNickname}.";
                    break;
                case FriendRequestResult.RequestAlreadySent:
                    message = $"A request has already been sent to {targetNickname}.";
                    break;
                case FriendRequestResult.RequestAlreadyReceived:
                    message = $"You have a pending request from {targetNickname}. Check your inbox!";
                    break;
                case FriendRequestResult.CannotAddSelf:
                    message = "You cannot send a friend request to yourself.";
                    break;
                case FriendRequestResult.Failed:
                default:
                    message = "The request could not be processed due to a server error or timeout.";
                    break;
            }
            
            _modalService.ShowAlert(title, message); 
        }

        private void OnFriendListUpdated(List<FriendData> updatedList)
        {
            _ = ExecuteRefreshFriendsAsync(); 
        }

        private void OnFriendActionNotification(string message, bool isSuccess)
        {
            _modalService.ShowAlert(isSuccess ? "Notification" : "Alert", message);
        }

        private void OnFriendRequestReceived(FriendRequestData newRequest)
        {
            _modalService.ShowAlert("New Request", $"You received a friend request from {newRequest.RequesterNickname}.");
        }

    }
}