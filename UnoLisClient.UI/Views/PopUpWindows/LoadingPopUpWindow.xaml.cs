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
