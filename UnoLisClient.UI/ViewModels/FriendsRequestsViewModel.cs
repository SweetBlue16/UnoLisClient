using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Friends;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using System.ServiceModel;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    /// <summary>
    /// ViewModel for FriendsRequestPage using services.
    /// Allows to see, accept or decline pending requests.
    /// </summary>
    public class FriendRequestsViewModel : BaseViewModel
    {
        private readonly IFriendsService _friendsService;

        public ObservableCollection<FriendRequestData> FriendRequests { get; } = new ObservableCollection<FriendRequestData>();

        private FriendRequestData _selectedRequest;
        public FriendRequestData SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                SetProperty(ref _selectedRequest, value);
                (AcceptRequestCommand as RelayCommandGeneric<FriendRequestData>)?.RaiseCanExecuteChanged();
                (RejectRequestCommand as RelayCommandGeneric<FriendRequestData>)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand RefreshRequestsCommand { get; }
        public ICommand AcceptRequestCommand { get; }
        public ICommand RejectRequestCommand { get; }

        public FriendRequestsViewModel(IFriendsService friendsService, IDialogService dialogService)
      : base(dialogService)
        {
            _friendsService = friendsService;

            _friendsService.Callback.FriendRequestReceivedEvent += OnFriendRequestReceived;

            RefreshRequestsCommand = new RelayCommand(async () => await ExecuteRefreshRequestsAsync(),
              () => !IsLoading);

            AcceptRequestCommand = new RelayCommandGeneric<FriendRequestData>(async (r) => await RespondToRequestAsync(r, true),
              (r) => r != null && !IsLoading);

            RejectRequestCommand = new RelayCommandGeneric<FriendRequestData>(async (r) => await RespondToRequestAsync(r, false),
              (r) => r != null && !IsLoading);

            _ = ExecuteRefreshRequestsAsync();
        }

        private async Task ExecuteRefreshRequestsAsync()
        {
            IsLoading = true;
            FriendRequests.Clear();
            try
            {
                var requests = await _friendsService.GetPendingRequestsAsync(CurrentSession.CurrentUserNickname);
                await App.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (var req in requests.OrderBy(r => r.RequesterNickname))
                        FriendRequests.Add(req);
                });
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Timeout loading pending requests: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Communication error loading pending requests: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error loading pending requests: {ex.Message}";
                HandleException(MessageCode.FriendsInternalError, logMessage, ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RespondToRequestAsync(FriendRequestData request, bool accepted)
        {
            if (request == null) return;

            var actionText = accepted ? "Accept" : "Decline";
            bool confirmed = _dialogService.ShowQuestionDialog(
                $"{actionText} Request",
                $"Are you sure you want to {actionText.ToLower()} the request from {request.RequesterNickname}?");

            if (!confirmed) return;

            IsLoading = true;
            try
            {
                bool success;
                if (accepted)
                {
                    success = await _friendsService.AcceptFriendRequestAsync(request);
                }
                else
                {
                    success = await _friendsService.RejectFriendRequestAsync(request);
                }

                if (success)
                {
                    await App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        FriendRequests.Remove(request);
                        SelectedRequest = null;

                        _dialogService.ShowAlert("Success", $"Request from {request.RequesterNickname} was {actionText.ToLower()}ed.");
                    });
                }
                else
                {
                    _dialogService.ShowAlert("Error", $"Could not {actionText.ToLower()} the request. Please try again.");
                }
            }
            catch (TimeoutException timeoutEx)
            {
                string logMessage = $"Error processing friend response: {timeoutEx.Message}";
                HandleException(MessageCode.Timeout, logMessage, timeoutEx);
            }
            catch (CommunicationException commEx)
            {
                string logMessage = $"Communication error processing friend response: {commEx.Message}";
                HandleException(MessageCode.ConnectionFailed, logMessage, commEx);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error processing friend response: {ex.Message}";
                HandleException(MessageCode.FriendsInternalError, logMessage, ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnFriendRequestReceived(FriendRequestData newRequest)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                FriendRequests.Add(newRequest);
            });
        }
    }
}