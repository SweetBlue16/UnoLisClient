using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for InputPopUpWindow.xaml
    /// </summary>
    public partial class InputPopUpWindow : Window
    {
        public string UserInput { get; private set; }

        public InputPopUpWindow(string title, string message, string watermark)
        {
            InitializeComponent();
            TitleLabel.Content = title;
            MessageTextBlock.Text = message;
            InputTextBox.Tag = watermark;
            Title = title.ToUpper();

            InputTextBox.Focus();
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.UserInput = InputTextBox.Text.Trim();
            this.DialogResult = true;
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.DialogResult = false;
        }

        private void InputTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (OkButton != null)
            {
                OkButton.IsEnabled = !string.IsNullOrWhiteSpace(InputTextBox.Text);
            }
        }
    }
}
