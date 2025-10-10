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
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GameSettingsPage());
        }

        private void SettingsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new SettingsPage());
        }

        private void ShopLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new AvatarShopPage());
        }

        private void ProfileLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new YourProfilePage());
        }

        private void ExitLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new GamePage());
        }
    }
}
