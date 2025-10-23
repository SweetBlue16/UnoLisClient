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
using UnoLisClient.UI.Utils; // si usas SoundManager o similares

namespace UnoLisClient.UI.Pages
{
    public partial class JoinMatchPage : Page
    {
        public JoinMatchPage()
        {
            InitializeComponent();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            string code = CodeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter a match code.", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // ⚡ Simulación: validación o conexión
            if (code.Length == 5) // Ejemplo: "ABX92"
            {
                new SimplePopUpWindow("Success", $"Successfully joined the {code} match!").ShowDialog();
                NavigationService?.Navigate(new MatchLobbyPage());
            }
            else
            {
                MessageBox.Show("Invalid match code.", "UNO LIS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            NavigationService?.Navigate(new MatchMenuPage());
        }
    }
}

