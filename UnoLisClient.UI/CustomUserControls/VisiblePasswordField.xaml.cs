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

namespace UnoLisClient.UI.CustomUserControls
{
    /// <summary>
    /// Interaction logic for VisiblePasswordField.xaml
    /// </summary>
    public partial class VisiblePasswordField : UserControl
    {
        private readonly Geometry _eyeOpen = Geometry.Parse("M1,10 C4,2 12,2 15,10 C12,18 4,18 1,10 Z M8,13 A3,3 0 1 1 8,7 A3,3 0 1 1 8,13 Z");
        private readonly Geometry _eyeClosed = Geometry.Parse("M1,10 C4,2 12,2 15,10 C12,18 4,18 1,10 Z M0,0 L16,16");
        private bool _isPasswordVisible = false;

        public VisiblePasswordField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(VisiblePasswordField),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set =>  SetValue(PasswordProperty, value);
        }

        private void VisiblePasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            Password = VisiblePasswordTextBox.Text;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }

        private void ClickTogglePasswordButton(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {
                PasswordBox.Password = VisiblePasswordTextBox.Text;
                VisiblePasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                EyeIcon.Data = _eyeOpen;
                EyeIcon.Fill = new SolidColorBrush(Color.FromRgb(255, 211, 105));
            }
            else
            {
                VisiblePasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                VisiblePasswordTextBox.Visibility = Visibility.Visible;
                EyeIcon.Data = _eyeClosed;
                EyeIcon.Fill = new SolidColorBrush(Color.FromRgb(61, 130, 240));
            }
            _isPasswordVisible = !_isPasswordVisible;
        }
    }
}
