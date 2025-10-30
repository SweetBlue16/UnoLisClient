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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnoLisClient.UI.Views.Controls
{
    /// <summary>
    /// Interaction logic for LoadingProgressBar.xaml
    /// </summary>
    public partial class LoadingProgressBar : UserControl
    {
        private bool _isRunning;

        public LoadingProgressBar()
        {
            InitializeComponent();
        }

        public void Start()
        {
            if (_isRunning)
            {
                return;
            }
            _isRunning = true;
            Visibility = Visibility.Visible;
            _ = AnimateProgressAsync();
        }

        public async void Stop()
        {
            _isRunning = false;
            MainProgressBar.Value = 100;
            await Task.Delay(300);
            Visibility = Visibility.Collapsed;
        }

        private async Task AnimateProgressAsync()
        {
            MainProgressBar.Value = 0;
            while (_isRunning && MainProgressBar.Value < 90)
            {
                MainProgressBar.Value += 3;
                await Task.Delay(100);
            }
        }
    }
}
