using System.Windows;
using System.Windows.Controls;
using UnoLisClient.Logic.Enums;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for InputPopUpWindow.xaml
    /// </summary>
    public partial class InputPopUpWindow : Window
    {
        public string UserInput { get; private set; }

        public InputPopUpWindow(string title, string message, string watermark, PopUpIconType icon = PopUpIconType.None)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            MessageTextBlock.Text = message;
            InputTextBox.Tag = watermark;
            Title = title.ToUpper();
            SetPopUpIcon(icon);

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
