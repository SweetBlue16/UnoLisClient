using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.UnoLisServerReference.Friends;
using UnoLisClient.Logic.Mappers;

namespace UnoLisClient.Logic.Services
{
    public class FriendsService :  IFriendsService
    {
        private static readonly Lazy<FriendsService> _instance =
            new Lazy<FriendsService>(() => new FriendsService());
        public static IFriendsService Instance => _instance.Value;
        private IFriendsManager _proxy;
        private DuplexChannelFactory<IFriendsManager> _factory;
        private string _currentUserNickname;
        public FriendsCallback Callback { get; }
        private FriendsService()
        {
            Callback = new FriendsCallback();
        }

        public void Initialize(string nickname)
        {
            if (_factory != null && _factory.State != CommunicationState.Opened)
            {
                Cleanup();
            }

            if (_factory != null)
            {
                return;
            }

            try
            {
                _currentUserNickname = nickname;
                var context = new InstanceContext(Callback);

                _factory = new DuplexChannelFactory<IFriendsManager>(context, "FriendsManagerEndpoint");
                _proxy = _factory.CreateChannel();

                if (_proxy is ICommunicationObject channel)
                {
                    ServerConnectionMonitor.Monitor(channel);
                }

                _proxy.SubscribeToFriendUpdates(_currentUserNickname);
            }
            catch (TimeoutException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FriendsService.Initialize] Timeout: {ex.Message}");
                Cleanup();
                throw;
            }
            catch (EndpointNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FriendsService.Initialize] Endpoint not found: {ex.Message}");
                Cleanup();
                throw;
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FriendsService.Initialize] Communication error: {ex.Message}");
                Cleanup();
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FriendsService.Initialize] Unexpected error: {ex.Message}");
                Cleanup();
                throw;
            }
        }

        public async Task<FriendRequestResult> SendFriendRequestAsync(string requesterNickname, string targetNickname)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está inicializado. " +
                "Llama a Initialize() primero.");

            try
            {
                return await _proxy.SendFriendRequestAsync(requesterNickname, targetNickname);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on SendFriendRequest: {ex.Message}");
                return FriendRequestResult.Failed;
            }
            catch (TimeoutException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Timeout on SendFriendRequest: {ex.Message}");
                return FriendRequestResult.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected Error on SendFriendRequest: {ex.Message}");
                return FriendRequestResult.Failed;
            }
        }

        public async Task<bool> AcceptFriendRequestAsync(FriendRequestData request)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está inicializado." +
                " Llama a Initialize() primero.");
            try
            {
                return await _proxy.AcceptFriendRequestAsync(request);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on AcceptFriendRequest: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on AcceptFriendRequest: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RejectFriendRequestAsync(FriendRequestData request)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está inicializado." +
                " Llama a Initialize() primero.");
            try
            {
                return await _proxy.RejectFriendRequestAsync(request);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RejectFriendRequest: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RejectFriendRequest: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveFriendAsync(FriendRequestData request)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está " +
                "inicializado. Llama a Initialize() primero.");
            try
            {
                return await _proxy.RemoveFriendAsync(request);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RemoveFriend: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RemoveFriend: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Friend>> GetFriendsListAsync(string nickname)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está inicializado. " +
                "Llama a Initialize() primero.");
            try
            {
                var resultDtos = await _proxy.GetFriendsListAsync(nickname);
                return resultDtos.Select(f => new Friend(f)).ToList();
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetFriendsList: {ex.Message}");
                return new List<Friend>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetFriendsList: {ex.Message}");
                return new List<Friend>();
            }
        }

        public async Task<List<FriendRequestData>> GetPendingRequestsAsync(string nickname)
        {
            if (_proxy == null) throw new InvalidOperationException("FriendsService no está inicializado. Llama a Initialize() primero.");
            try
            {
                var resultDtos = await _proxy.GetPendingRequestsAsync(nickname);
                return resultDtos.ToList();
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetPendingRequests: {ex.Message}");
                return new List<FriendRequestData>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetPendingRequests: {ex.Message}");
                return new List<FriendRequestData>();
            }
        }

        public void Cleanup()
        {
            if (_factory == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_currentUserNickname) && _proxy != null)
            {
                try
                {
                    if (_factory.State == CommunicationState.Opened)
                    {
                        _proxy.UnsubscribeFromFriendUpdates(_currentUserNickname);
                    }
                }
                catch (CommunicationException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[FriendsService.Cleanup] Communication error " +
                        $"unsubscribing: {ex.Message}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[FriendsService.Cleanup] Error unsubscribing: {ex.Message}");
                }
            }
            try
            {
                if (_factory.State == CommunicationState.Opened)
                {
                    _factory.Close();
                }
                else
                {
                    _factory.Abort();
                }
            }
            catch (TimeoutException)
            {
                _factory.Abort();
            }
            catch (CommunicationException)
            {
                _factory.Abort();
            }
            catch (Exception)
            {
                _factory.Abort();
            }
            finally
            {
                _proxy = null;
                _factory = null;
                _currentUserNickname = null;
            }
        }
    }
}