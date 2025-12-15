using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Shop;

namespace UnoLisClient.Logic.Callbacks
{
    public class ShopCallback : IShopManagerCallback
    {
        public event Action<List<ShopItem>> ShopItemsReceivedEvent;
        public event Action<ShopPurchaseResult> PurchaseResponseReceivedEvent;
        public event Action<int> PlayerBalanceReceivedEvent;

        public void ShopItemsReceived(ShopItem[] items)
        {
            List<ShopItem> itemList = items != null ? items.ToList() : new List<ShopItem>();
            ShopItemsReceivedEvent?.Invoke(itemList);
        }

        public void PurchaseResponse(ShopPurchaseResult result)
        {
            PurchaseResponseReceivedEvent?.Invoke(result);
        }

        public void PlayerBalanceReceived(int balance)
        {
            PlayerBalanceReceivedEvent?.Invoke(balance);
        }
    }
}
