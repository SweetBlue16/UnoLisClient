using System;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.Services;
using UnoLisClient.Logic.UnoLisServerReference.Shop;
using UnoLisClient.UI.Commands; 
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Services;

namespace UnoLisClient.UI.ViewModels
{
    public class AvatarShopViewModel : BaseViewModel 
    {
        private int _currentRevoCoins;
        public int CurrentRevoCoins
        {
            get => _currentRevoCoins;
            set => SetProperty(ref _currentRevoCoins, value);
        }

        private readonly ShopService _shopService;
        private string _currentUserNickname;

        private const int SpecialBoxId = 6;
        private const int EpicBoxId = 7;
        private const int LegendaryBoxId = 8;

        public ICommand BuySpecialCommand { get; }
        public ICommand BuyEpicCommand { get; }
        public ICommand BuyLegendaryCommand { get; }

        public AvatarShopViewModel(IDialogService dialogService) : base(dialogService)
        {
            _shopService = ShopService.Instance;
            _shopService.OnPurchaseCompleted += HandlePurchaseResult;

            _shopService.OnBalanceUpdated += (coins) =>
            {
                CurrentRevoCoins = coins;
            };

            _currentUserNickname = CurrentSession.CurrentUserNickname;

            BuySpecialCommand = new RelayCommand(() => PurchaseBox(SpecialBoxId));
            BuyEpicCommand = new RelayCommand(() => PurchaseBox(EpicBoxId));
            BuyLegendaryCommand = new RelayCommand(() => PurchaseBox(LegendaryBoxId));

            LoadUserData();
        }

        private async void LoadUserData()
        {
            await _shopService.GetBalanceAsync(_currentUserNickname);
        }

        private async void PurchaseBox(int boxId)
        {
            SoundManager.PlayClick();
            await _shopService.PurchaseBoxAsync(_currentUserNickname, boxId);
        }

        private void HandlePurchaseResult(ShopPurchaseResult result)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (result.IsSuccess)
                {
                    string avatarName = result.WonAvatar.AvatarName;
                    string imagePath = $"pack://application:,,,/Avatars/{avatarName}.png";
                    string title = "¡NUEVO AVATAR!";
                    string msg = $"¡Ganaste: {result.WonAvatar.AvatarName}!\nRareza: {result.WonAvatar.Rarity}";

                    new AvatarWonPopUpWindow(title, msg, imagePath).ShowDialog();
                    CurrentRevoCoins = result.RemainingCoins;
                }
                else
                {
                    string msg = GetErrorMessage(result.MessageCode);
                    new SimplePopUpWindow("Ups...", msg, PopUpIconType.Error).ShowDialog();
                }
            });
        }

        private string GetErrorMessage(string code)
        {
            switch (code)
            {
                case "InsufficientFunds": return "No tienes suficientes Revo Coins.";
                case "AllContentOwned": return "¡Ya tienes todos los avatares de esta caja!";
                case "BoxNotFound": return "Esta caja ya no está disponible.";
                default: return "Ocurrió un error al procesar la compra.";
            }
        }
    }
}
