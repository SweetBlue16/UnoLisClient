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
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Utils;


namespace UnoLisClient.UI.Pages
{
    /// <summary>
    /// Interaction logic for JoinMatchPage.xaml
    /// </summary>
    public partial class JoinMatchPage : Page
    {
        public JoinMatchPage()
        {
            InitializeComponent();
        }

        private void ClickJoinButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            string code = CodeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter a match code.", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Simulación: validación o conexión
            //TODO: Implementar la lógica real para unirse a una partida usando el código proporcionado.
            if (code.Length == 5) 
            {
                new SimplePopUpWindow("Success", $"Successfully joined the {code} match!").ShowDialog();
                NavigationService?.Navigate(new MatchLobbyPage());
            }
            else
            {
                MessageBox.Show("Invalid match code.", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new MatchMenuPage());
        }
    }
}

