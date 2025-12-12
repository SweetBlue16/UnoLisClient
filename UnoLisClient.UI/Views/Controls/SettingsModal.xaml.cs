using System;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Utilities;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.Views.Controls
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

            var questionPopup = new QuestionPopUpWindow(Global.ConfirmationLabel,
                Match.ExitMatchConfirmationLabel,
                PopUpIconType.Logout);
            bool? result = questionPopup.ShowDialog();

            if (result == true)
            {
                LeaveMatchRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                string cancelFile = "cancel.wav";
                double volume = 0.5;
                SoundManager.PlaySound(cancelFile, volume);
            }
        }
    }
}
