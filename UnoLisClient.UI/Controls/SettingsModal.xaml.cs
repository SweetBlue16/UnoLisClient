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

namespace UnoLisClient.UI.Controls
{
    public partial class SettingsModal : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LeaveMatchRequested;

        public SettingsModal()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void LeaveMatchButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            var questionPopup = new QuestionPopUpWindow("Confirm", "Are you sure you want to leave the match?");
            bool? result = questionPopup.ShowDialog();

            if (result == true)
            {
                // Avisa al "padre" que el usuario confirmó la salida
                LeaveMatchRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SoundManager.PlaySound("cancel.wav", 0.5);
            }
        }
    }
}
