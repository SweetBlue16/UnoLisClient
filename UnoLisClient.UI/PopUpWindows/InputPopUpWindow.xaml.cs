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
        public InputPopUpWindow(string title, string message, string watermark)
        {
            InitializeComponent();
            TitleLabel.Content = title;
            MessageTextBlock.Text = message;
            InputTextBox.Tag = watermark;
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            // TO-DO Logic with the text box
            this.Close();
            string userInput = InputTextBox.Text.Trim().ToUpper();
        }
    }
}
