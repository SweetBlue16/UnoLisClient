using System.Windows;
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for LoadingPopUpWindow.xaml
    /// </summary>
    public partial class LoadingPopUpWindow : Window
    {
        public LoadingPopUpWindow()
        {
            InitializeComponent();
            Title = Global.LoadingLabel.ToUpper();
            Loaded += LoadingPopUpWindowLoaded;
        }

        public void StopLoadingAndClose()
        {
            Dispatcher.Invoke(() =>
            {
                LoadingProgressBar.Stop();
                this.Close();
            });
        }

        private void LoadingPopUpWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadingProgressBar.Start();
        }
    }
}
