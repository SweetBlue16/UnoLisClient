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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.GoBack();
        }

        private void CreateGameButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            new SimplePopUpWindow("Game Created", "Your game has been successfully created!").ShowDialog();
            NavigationService?.Navigate(new MatchLobbyPage());
        }
        private void MusicToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🎵"; // Encendido
            // Aquí: reproducir música
        }

        private void MusicToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🔇"; // Apagado
            // Aquí: pausar/detener música
        }

        private void SoundToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔊"; // Encendido
            // Aquí: habilitar efectos de sonido
        }

        private void SoundToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔈"; // Apagado
            // Aquí: deshabilitar efectos de sonido
        }
    }
}
