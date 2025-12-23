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
using UnoLisClient.Logic.Models;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private const int MinGuestNumber = 1000;
        private const int MaxGuestNumber = 9999;

        public GamePage()
        {
            InitializeComponent();
        }

        private void ClickPlayGuestButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            int randomSuffix = new Random().Next(MinGuestNumber, MaxGuestNumber);
            CurrentSession.CurrentUserNickname = $"Guest_{randomSuffix}";
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new LoginPage());
        }

        private void ClickSettingsButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new SettingsPage());
        }
    }
}
