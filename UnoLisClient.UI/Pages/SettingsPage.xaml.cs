using System.Globalization;
using System.Threading;
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

            // 1) Cargar idioma guardado (fallback en-US)
            var saved = Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(saved))
                saved = "en-US";

            // 2) Aplicar cultura global
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(saved);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(saved);

            // 3) Seleccionar item en el combo
            SelectLanguageByCode(saved);

            _initializing = false;
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

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_initializing) return;

            if (LanguageComboBox.SelectedItem is ComboBoxItem item &&
                item.Tag is string newLangCode)
            {
                if (Properties.Settings.Default.languageCode == newLangCode) return;

                // Guardar preferencia
                Properties.Settings.Default.languageCode = newLangCode;
                Properties.Settings.Default.Save();

                // ✅ Cambiar cultura global
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(newLangCode);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(newLangCode);

                // ✅ Recargar la página
                Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    NavigationService?.Navigate(new SettingsPage());
                }), DispatcherPriority.ApplicationIdle);
            }
        }
    }
}
