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
    public class FriendsService : IDisposable, IFriendsService
    {

        private readonly IFriendsManager _proxy;
        private readonly DuplexChannelFactory<IFriendsManager> _factory;
        public FriendsCallback Callback { get; } = new FriendsCallback();
        private bool _disposed = false;
        public FriendsService()
        {
            var context = new InstanceContext(Callback);
            _factory = new DuplexChannelFactory<IFriendsManager>(context, "FriendsManagerEndpoint");
            _proxy = _factory.CreateChannel();
        }

        /// <summary>
        /// Envía una solicitud de amistad y espera el resultado de negocio (Success/Error Code) del servidor.
        /// </summary>
        public async Task<FriendRequestResult> SendFriendRequestAsync(string requesterNickname, string targetNickname)
        {
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

        /// <summary>
        /// Acepta una solicitud de amistad.
        /// </summary>
        public async Task<bool> AcceptFriendRequestAsync(FriendRequestData request)
        {
            try
            {
                return await _proxy.AcceptFriendRequestAsync(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on AcceptFriendRequest: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Rechaza una solicitud de amistad.
        /// </summary>
        public async Task<bool> RejectFriendRequestAsync(FriendRequestData request)
        {
            try
            {
                return await _proxy.RejectFriendRequestAsync(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RejectFriendRequest: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina una amistad confirmada.
        /// </summary>
        public async Task<bool> RemoveFriendAsync(FriendRequestData request)
        {
            try
            {
                return await _proxy.RemoveFriendAsync(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on RemoveFriend: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Obtiene la lista de amigos confirmados del jugador.
        /// </summary>
        public async Task<List<Friend>> GetFriendsListAsync(string nickname)
        {
            try
            {
                var resultDtos = await _proxy.GetFriendsListAsync(nickname);
                // Mapeo: Convertir DTOs WCF a modelos de dominio Friend (Local)
                return resultDtos.Select(f => new Friend(f)).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetFriendsList: {ex.Message}");
                return new List<Friend>();
            }
        }

        /// <summary>
        /// Obtiene la lista de solicitudes de amistad pendientes.
        /// </summary>
        public async Task<List<FriendRequestData>> GetPendingRequestsAsync(string nickname)
        {
            try
            {
                var resultDtos = await _proxy.GetPendingRequestsAsync(nickname);
                return resultDtos.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WCF Error on GetPendingRequests: {ex.Message}");
                return new List<FriendRequestData>();
            }
        }

        /// <summary>
        /// Inicia la suscripción Duplex a notificaciones del servidor.
        /// </summary>
        public void Subscribe(string nickname) => _proxy.SubscribeToFriendUpdates(nickname);

        /// <summary>
        /// Finaliza la suscripción Duplex.
        /// </summary>
        public void Unsubscribe(string nickname) => _proxy.UnsubscribeFromFriendUpdates(nickname);

        /// <summary>
        /// Implementación de IDisposable para cerrar el canal WCF.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            try
            {
                if (_factory != null)
                {
                    if (_factory.State == CommunicationState.Opened)
                        _factory.Close();
                    else
                        _factory.Abort();
                }
            }
            catch
            {
                _factory?.Abort();
            }

            _disposed = true;
        }
    }
}