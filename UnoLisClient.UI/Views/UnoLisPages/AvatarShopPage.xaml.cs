using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for AvatarShopPage.xaml
    /// </summary>
    public partial class AvatarShopPage : Page
    {
        public AvatarShopPage()
        {
            InitializeComponent();
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void ClickBuySpecial(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow(Shop.CongratulationsLabel,
                string.Format(Shop.PurchaseSuccessfulLabel, Shop.SpecialBoxLabel),
                PopUpIconType.Success)
            .ShowDialog();
        }

        private void ClickBuyEpic(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow(Shop.CongratulationsLabel,
                string.Format(Shop.PurchaseSuccessfulLabel, Shop.EpicBoxLabel),
                PopUpIconType.Success)
            .ShowDialog();
        }

        private void ClickBuyLegendary(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow(Shop.CongratulationsLabel,
                string.Format(Shop.PurchaseSuccessfulLabel, Shop.LegendaryBoxLabel),
                PopUpIconType.Success)
            .ShowDialog();
        }
    }
}
