using System;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Shop;

namespace UnoLisClient.Logic.Services
{
    public class ShopService
    {
        private static readonly Lazy<ShopService> _instance =
            new Lazy<ShopService>(() => new ShopService());
        public static ShopService Instance => _instance.Value;

        private ShopManagerClient _proxy;
        private readonly ShopCallback _callback;

        public event Action<ShopPurchaseResult> OnPurchaseCompleted;
        public event Action<int> OnBalanceUpdated;

        private ShopService()
        {
            _callback = new ShopCallback();
            _callback.PurchaseResponseReceivedEvent += (result) => OnPurchaseCompleted?.Invoke(result);
            _callback.PlayerBalanceReceivedEvent += (balance) => OnBalanceUpdated?.Invoke(balance);
        }

        private void EnsureConnection()
        {
            if (_proxy == null || _proxy.State != CommunicationState.Opened)
            {
                var context = new InstanceContext(_callback);
                _proxy = new ShopManagerClient(context, "NetTcpBinding_IShopManager");
                ServerConnectionMonitor.Monitor(_proxy.InnerChannel);
            }
        }

        public Task PurchaseBoxAsync(string nickname, int boxId)
        {
            return Task.Run(() =>
            {
                try
                {
                    EnsureConnection();

                    var request = new PurchaseRequest
                    {
                        Nickname = nickname,
                        ItemId = boxId 
                    };

                    _proxy.PurchaseItem(request);
                }
                catch (CommunicationException commEx)
                {
                    Logger.Error($"[SHOP] Communication error while buying box {boxId}", commEx);
                }
                catch (TimeoutException timeoutEx)
                {
                    Logger.Error($"[SHOP] Timeout error while buying box {boxId}", timeoutEx);
                }
                catch (Exception ex)
                {
                    Logger.Error($"[SHOP] Error buying box {boxId}", ex);
                }
            });
        }

        public Task GetBalanceAsync(string nickname)
        {
            return Task.Run(() =>
            {
                try
                {
                    EnsureConnection();
                    _proxy.GetPlayerBalance(nickname);
                }
                catch (CommunicationException commEx)
                {
                    Logger.Warn($"[SHOP] Connection unstable fetching balance for {nickname}: {commEx.Message}");
                }
                catch (TimeoutException timeoutEx)
                {
                    Logger.Warn($"[SHOP] Timeout fetching balance: {timeoutEx.Message}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"[SHOP] Unexpected error fetching balance", ex);
                }
            });
        }
    }
}
