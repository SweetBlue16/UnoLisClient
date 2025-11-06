using System.Windows;
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
