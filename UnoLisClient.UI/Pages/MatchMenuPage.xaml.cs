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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Utils; // si usas SoundManager u otras utilidades

namespace UnoLisClient.UI.Pages
{
    public partial class MatchMenuPage : Page
    {
        public MatchMenuPage()
        {
            InitializeComponent();
        }

        private void CreateMatchButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick(); // si tienes el efecto
            NavigationService?.Navigate(new GameSettingsPage()); // te lleva al lobby
        }

        private void JoinMatchButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick(); // idem
            NavigationService?.Navigate(new JoinMatchPage()); // (luego creamos esta)
        }
    }
}

