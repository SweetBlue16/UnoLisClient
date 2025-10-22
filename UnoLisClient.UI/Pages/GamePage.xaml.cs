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
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.Utils;

namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
        }

        private void PlayGuestButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            CurrentSession.CurrentUserNickname = "Guest";
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new LoginPage());
        }
    }
}
