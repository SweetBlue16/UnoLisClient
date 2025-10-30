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
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for QuestionPopUpWindow.xaml
    /// </summary>
    public partial class QuestionPopUpWindow : Window
    {
        public QuestionPopUpWindow(string title, string message)
        {
            InitializeComponent();
            TitleLabel.Content = title;
            MessageTextBlock.Text = message;
        }

        private void ClickNoButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.DialogResult = false;
        }

        private void ClickYesButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.DialogResult = true;
        }
    }
}
