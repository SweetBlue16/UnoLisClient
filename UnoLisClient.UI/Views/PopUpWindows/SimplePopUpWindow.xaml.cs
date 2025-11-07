using System.Windows;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for SimplePopUpWindow.xaml
    /// </summary>
    public partial class SimplePopUpWindow : Window
    {
        public SimplePopUpWindow(string title, string message)
        {
            InitializeComponent();
            TitleLabel.Content = title;
            MessageTextBlock.Text = message;
            Title = title.ToUpper();
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Close();
        }
    }
}
