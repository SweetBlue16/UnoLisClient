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
using System.Windows.Shapes;

namespace UnoLisClient.UI.PopUpWindows
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

            InputTextBox.Focus();
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            this.UserInput = InputTextBox.Text.Trim();
            this.DialogResult = true;
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
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
