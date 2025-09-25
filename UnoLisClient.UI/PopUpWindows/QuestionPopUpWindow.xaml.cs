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
            this.Close();
        }

        private void ClickYesButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
