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
using UnoLisClient.UI.Utilities;
using System.Windows.Shapes;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for AvatarWonPopUpWindow.xaml
    /// </summary>
    public partial class AvatarWonPopUpWindow : Window
    {
        public AvatarWonPopUpWindow(string title, string message, string imagePath)
        {
            InitializeComponent();

            TitleLabel.Text = title.ToUpper();
            MessageTextBlock.Text = message;

            LoadAvatarImage(imagePath);
        }

        private void LoadAvatarImage(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    AvatarImage.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading avatar image: {ex.Message}");
            }
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Close();
        }
    }
}
