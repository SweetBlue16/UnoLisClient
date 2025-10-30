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

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for MatchMenuPage.xaml
    /// </summary>
    public partial class MatchMenuPage : Page
    {
        public MatchMenuPage()
        {
            InitializeComponent();
        }

        private void ClickCreateMatchButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new GameSettingsPage()); 
        }

        private void ClickJoinMatchButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick(); 
            NavigationService?.Navigate(new JoinMatchPage()); 
        }
    }
}

