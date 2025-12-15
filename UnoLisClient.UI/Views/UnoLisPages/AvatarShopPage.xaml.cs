using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UnoLisClient.UI.Services; 
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

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
            this.DataContext = new AvatarShopViewModel(new AlertManager());
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }
    }
}
