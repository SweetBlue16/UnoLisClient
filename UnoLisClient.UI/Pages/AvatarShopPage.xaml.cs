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
            new SimplePopUpWindow("Purchase Successful", "You bought a Special Box!").ShowDialog();
        }

        private void ClickBuyEpic(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Purchase Successful", "You bought an Epic Box!").ShowDialog();
        }

        private void ClickBuyLegendary(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Purchase Successful", "You bought a Legendary Box!").ShowDialog();
        }
    }
}
