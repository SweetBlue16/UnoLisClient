using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace UnoLisClient.UI.Views.Controls.Animations
{
    public partial class SpriteAnimationControl : UserControl
    {
        private readonly DispatcherTimer _timer;
        private List<ImageSource> _frames;
        private int _currentFrameIndex;
        private int _currentLoopCount;

        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(SpriteAnimationControl),
                new PropertyMetadata(false, OnIsPlayingChanged));

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public string SpriteSource { get; set; }
        public int Columns { get; set; } = 6; 
        public int Rows { get; set; } = 6;
        public int FrameRate { get; set; } = 24;
        public int LoopCount { get; set; } = 1;

        public SpriteAnimationControl()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += OnFrameTick;
        }

        private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SpriteAnimationControl)d;
            bool isPlaying = (bool)e.NewValue;

            if (isPlaying)
            {
                if (control._frames == null || control._frames.Count == 0)
                {
                    control.LoadFrames();
                }

                if (control._frames != null && control._frames.Count > 0)
                {
                    control.StartAnimation();
                }
                else
                {
                    control.IsPlaying = false;
                }
            }
            else
            {
                control.StopAnimation();
            }
        }

        private void LoadFrames()
        {
            if (string.IsNullOrEmpty(SpriteSource)) return;

            try
            {
                var uri = new Uri(SpriteSource, UriKind.RelativeOrAbsolute);
                var bitmap = new BitmapImage(uri);

                int frameWidth = bitmap.PixelWidth / Columns;
                int frameHeight = bitmap.PixelHeight / Rows;

                _frames = new List<ImageSource>();

                for (int y = 0; y < Rows; y++)
                {
                    for (int x = 0; x < Columns; x++)
                    {
                        var rect = new Int32Rect(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                        var cropped = new CroppedBitmap(bitmap, rect);
                        cropped.Freeze();
                        _frames.Add(cropped);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading sprite: {ex.Message}");
            }
        }

        private void StartAnimation()
        {
            if (_frames == null || _frames.Count == 0) return;

            _currentFrameIndex = 0;
            DisplayImage.Source = _frames[0];
            this.Visibility = Visibility.Visible;

            _timer.Interval = TimeSpan.FromSeconds(1.0 / FrameRate);
            _timer.Start();
        }

        private void StopAnimation()
        {
            _timer.Stop();
            this.Visibility = Visibility.Collapsed;
        }

        private void OnFrameTick(object sender, EventArgs e)
        {
            _currentFrameIndex++;

            if (_currentFrameIndex >= _frames.Count)
            {
                _currentLoopCount++;

                if (_currentLoopCount < LoopCount)
                {
                    _currentFrameIndex = 0;
                }
                else
                {
                    StopAnimation();
                    IsPlaying = false;
                    return;
                }
            }

            if (_frames != null && _frames.Count > 0)
            {
                DisplayImage.Source = _frames[_currentFrameIndex];
            }
        }
    }
}