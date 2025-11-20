using System.Windows;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for QuestionPopUpWindow.xaml
    /// </summary>
    public partial class QuestionPopUpWindow : Window
    {
        public QuestionPopUpWindow(string title, string message, PopUpIconType icon = PopUpIconType.None)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            MessageTextBlock.Text = message;
            Title = title.ToUpper();
            SetPopUpIcon(icon);
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

        private void SetPopUpIcon(PopUpIconType icon)
        {
            var iconSource = PopUpIconHelper.GetIconSource(icon);

            if (iconSource != null)
            {
                IconImage.Source = iconSource;
                IconImage.Visibility = Visibility.Visible;
            }
            else
            {
                IconImage.Visibility = Visibility.Collapsed;
            }
        }
    }
}
