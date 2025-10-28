using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace UnoLisClient.UI.Pages
{
    public partial class SettingsPage : Page
    {
        private bool _initializing = true;

        public SettingsPage()
        {
            InitializeComponent();

            var saved = Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(saved))
                saved = "en-US";

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(saved);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(saved);
            SelectLanguageByCode(saved);

            if (Properties.Settings.Default.lastVolume > 0)
                VolumeSlider.Value = Properties.Settings.Default.lastVolume;
            else
                VolumeSlider.Value = 50;

            _initializing = false;
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenuPage());
        }

        private void SelectLanguageByCode(string code)
        {
            for (int i = 0; i < LanguageComboBox.Items.Count; i++)
            {
                if (LanguageComboBox.Items[i] is ComboBoxItem item &&
                    item.Tag is string tag &&
                    tag == code)
                {
                    LanguageComboBox.SelectedIndex = i;
                    return;
                }
            }
            LanguageComboBox.SelectedIndex = 0;
        }

        private void SelectionChangedLanguageComboBox(object sender, SelectionChangedEventArgs e)
        {
            if (_initializing) return;

            if (LanguageComboBox.SelectedItem is ComboBoxItem item &&
                item.Tag is string newLangCode)
            {
                if (Properties.Settings.Default.languageCode == newLangCode) return;

                Properties.Settings.Default.languageCode = newLangCode;
                Properties.Settings.Default.Save();

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(newLangCode);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(newLangCode);

                Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    NavigationService?.Navigate(new SettingsPage());
                }), DispatcherPriority.ApplicationIdle);
            }
        }

        private void ValueChangedVolumeSlider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_initializing) return;

            var mainWindow = Application.Current.MainWindow as UnoLisClient.UI.MainWindow;
            mainWindow?.SetMusicVolume(e.NewValue);

            Properties.Settings.Default.lastVolume = e.NewValue;
            Properties.Settings.Default.Save();
        }

    }
}
