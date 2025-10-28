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
    /// Interaction logic for GameSettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page
    {
        public GameSettingsPage()
        {
            InitializeComponent();
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void ClickCreateGameButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Game Created", "Your game has been successfully created!").ShowDialog();
            NavigationService?.Navigate(new MatchLobbyPage());
        }
        private void CheckedMusicToggle(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🎵"; 
        }

        private void UncheckedMusicToggle(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🔇";
            // Aquí: pausar/detener música
            // NOTA: Mejor silenciarla que detenerla para evitar retrasos al reanudar
            //TODO: Implementar la funcionalidad
        }

        private void CheckedSoundToggle(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔊";
        }

        private void CheckedSoundTogglex(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔈";

        }
    }
}
