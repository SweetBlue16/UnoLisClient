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
        private const int MaxProgress = 100;
        private const int BeforeMaxProgress = 90;
        private const int ProgressIncrement = 3;
        private const int StopDelayMilliseconds = 300;
        private const int AnimationDelayMilliseconds = 100;

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
            MainProgressBar.Value = MaxProgress;
            await Task.Delay(StopDelayMilliseconds);
            Visibility = Visibility.Collapsed;
        }

        private async Task AnimateProgressAsync()
        {
            MainProgressBar.Value = 0;
            while (_isRunning && MainProgressBar.Value < BeforeMaxProgress)
            {
                MainProgressBar.Value += ProgressIncrement;
                await Task.Delay(AnimationDelayMilliseconds);
            }
        }
    }
}
