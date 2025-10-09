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

namespace UnoLisClient.UI.Pages
{
    public partial class AvatarShopPage : Page
    {
        public AvatarShopPage()
        {
            InitializeComponent();
        }

        private void BuySpecial_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You bought a Special Box!");
        }

        private void BuyEpic_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You bought an Epic Box!");
        }

        private void BuyLegendary_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You bought a Legendary Box!");
        }
    }
}
