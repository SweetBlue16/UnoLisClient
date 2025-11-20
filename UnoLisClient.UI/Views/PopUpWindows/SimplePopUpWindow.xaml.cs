using System.Windows;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for SimplePopUpWindow.xaml
    /// </summary>
    public partial class SimplePopUpWindow : Window
    {
        public SimplePopUpWindow(string title, string message, PopUpIconType icon = PopUpIconType.None)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            MessageTextBlock.Text = message;
            Title = title.ToUpper();
            SetPopUpIcon(icon);
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Close();
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
