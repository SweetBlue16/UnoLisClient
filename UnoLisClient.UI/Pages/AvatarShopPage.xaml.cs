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
using UnoLisClient.UI.PopUpWindows;
using UnoLisClient.UI.Utils;

namespace UnoLisClient.UI.Pages
{
    public partial class AvatarShopPage : Page
    {
        public AvatarShopPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void BuySpecial_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Purchase Successful", "You bought a Special Box!").ShowDialog();
        }

        private void BuyEpic_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Purchase Successful", "You bought an Epic Box!").ShowDialog();
        }

        private void BuyLegendary_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Purchase Successful", "You bought a Legendary Box!").ShowDialog();
        }
    }
}
