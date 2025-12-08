using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Friends;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using System.ServiceModel;
using UnoLisServer.Common.Enums;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Properties.Langs;

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
                if (requests != null || requests.Count > 0)
                {
                    await App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var req in requests.OrderBy(r => r.RequesterNickname))
                            FriendRequests.Add(req);
                    });
                }
                else
                {
                    _dialogService.ShowWarning(ErrorMessages.FriendsInternalErrorMessageLabel);
                }
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

            var actionText = accepted ? FriendsList.FriendRequestAcceptButton : FriendsList.FriendRequestDeclineButton;
            bool confirmed = _dialogService.ShowQuestionDialog(
                string.Format(FriendsList.ActionRequestMessageLabel, actionText),
                string.Format(FriendsList.ConfirmationRequestMessageLabel, actionText.ToLower(), request.RequesterNickname),
                PopUpIconType.Question);

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
                        string message = string.Format(FriendsList.RequestSuccessMessageLabel, request.RequesterNickname, actionText.ToLower());
                        _dialogService.ShowAlert(Global.SuccessLabel, message, PopUpIconType.Success);
                    });
                }
                else
                {
                    string message = string.Format(ErrorMessages.CouldNotProcessRequestMessageLabel, actionText.ToLower());
                    _dialogService.ShowAlert(Global.OopsLabel, message, PopUpIconType.Warning);
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