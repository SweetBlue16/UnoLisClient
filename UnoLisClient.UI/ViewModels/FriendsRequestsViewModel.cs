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
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;

namespace UnoLisClient.UI.ViewModels
{
    /// <summary>
    /// ViewModel para la página de solicitudes de amistad, usando servicios inyectados.
    /// Permite ver, aceptar y rechazar solicitudes pendientes de forma asíncrona.
    /// </summary>
    public class FriendRequestsViewModel : BaseViewModel
    {
        private readonly IFriendsService _friendsService;
        private readonly IModalService _modalService;

        public ObservableCollection<FriendRequestData> FriendRequests { get; } = new ObservableCollection<FriendRequestData>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private FriendRequestData _selectedRequest;
        public FriendRequestData SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                SetProperty(ref _selectedRequest, value);
                (AcceptRequestCommand as RelayCommand<FriendRequestData>)?.RaiseCanExecuteChanged();
                (RejectRequestCommand as RelayCommand<FriendRequestData>)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand RefreshRequestsCommand { get; }
        public ICommand AcceptRequestCommand { get; }
        public ICommand RejectRequestCommand { get; }

        public FriendRequestsViewModel(IFriendsService friendsService, IModalService modalService)
        {
            _friendsService = friendsService;
            _modalService = modalService;

            _friendsService.Callback.FriendActionNotificationEvent += OnFriendActionNotification;
            _friendsService.Callback.FriendRequestReceivedEvent += OnFriendRequestReceived;

            RefreshRequestsCommand = new RelayCommand(async () => await ExecuteRefreshRequestsAsync(),
                () => !IsLoading);

            AcceptRequestCommand = new RelayCommand<FriendRequestData>(async (r) => await RespondToRequestAsync(r, true),
                (r) => r != null && !IsLoading);

            RejectRequestCommand = new RelayCommand<FriendRequestData>(async (r) => await RespondToRequestAsync(r, false),
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
            catch (Exception ex)
            {
                LogManager.Error($"Error loading pending requests: {ex.Message}", ex);
                _modalService.ShowAlert("Error", "Could not retrieve pending requests due to a server error.");
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
            var confirmResult = _modalService.ShowConfirmation(
                $"{actionText} Request",
                $"Are you sure you want to {actionText.ToLower()} the request from {request.RequesterNickname}?");

            if (confirmResult != MessageBoxResult.Yes) return;

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

                        _modalService.ShowAlert("Success", $"Request from {request.RequesterNickname} was {actionText.ToLower()}ed.");
                    });
                }
                else
                {
                    _modalService.ShowAlert("Error", $"Could not {actionText.ToLower()} the request. Please try again.");
                }
            }
            catch (Exception ex)
            {
                LogManager.Error($"Error processing friend response: {ex.Message}", ex);
                _modalService.ShowAlert("Error", "A fatal error occurred while processing the response.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private static void OnFriendActionNotification(string message, bool isSuccess)
        {
            LogManager.Info($"Action Notification Received: {message}");
        }

        private void OnFriendRequestReceived(FriendRequestData newRequest)
        {
            // El servidor nos envió el DTO completo.
            // NO necesitamos recargar toda la lista (ExecuteRefreshRequestsAsync).

            // Añadimos el nuevo objeto directamente a la ObservableCollection.
            // Usamos el Dispatcher para asegurar que ocurra en el Hilo de la UI.
            App.Current.Dispatcher.Invoke(() =>
            {
                FriendRequests.Add(newRequest);

                // Opcional: Mostrar la alerta aquí
                _modalService.ShowAlert("New Request", $"You received a friend request from {newRequest.RequesterNickname}.");
            });
        }
    }
}