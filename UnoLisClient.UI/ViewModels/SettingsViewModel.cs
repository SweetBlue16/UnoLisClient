using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.UI.Views.UnoLisWindows;

namespace UnoLisClient.UI.ViewModels
{
    public class LanguageItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class SettingsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly Page _view;

        public ObservableCollection<LanguageItem> AvailableLanguages { get; set; }

        private LanguageItem _selectedLanguage;
        public LanguageItem SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetProperty(ref _selectedLanguage, value))
                {
                    ChangeLanguage(value.Code);
                }
            }
        }

        private double _volume;
        public double Volume
        {
            get => _volume;
            set
            {
                if (SetProperty(ref _volume, value))
                {
                    ChangeVolume(value);
                }
            }
        }

        public ICommand CloseCommand { get; }

        public SettingsViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _view = view;
            _navigationService = (INavigationService)view;
            CloseCommand = new RelayCommand(ExecuteClose);

            LoadSettings();
        }

        private void LoadSettings()
        {
            AvailableLanguages = new ObservableCollection<LanguageItem>
            {
                new LanguageItem { Name = Settings.EnglishComboItem, Code = "en-US" },
                new LanguageItem { Name = Settings.SpanishComboItem, Code = "es-MX" }
            };

            var savedLang = Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(savedLang))
            {
                savedLang = "en-US";
            }

            _selectedLanguage = AvailableLanguages.FirstOrDefault(l => l.Code == savedLang) ?? AvailableLanguages[0];
            OnPropertyChanged(nameof(SelectedLanguage));

            double savedVol = Properties.Settings.Default.lastVolume;
            _volume = savedVol > 0 ? savedVol : 50;
            OnPropertyChanged(nameof(Volume));
        }

        private void ChangeLanguage(string newCode)
        {
            if (Properties.Settings.Default.languageCode == newCode)
            {
                return;
            }

            Properties.Settings.Default.languageCode = newCode;
            Properties.Settings.Default.Save();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(newCode);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(newCode);

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _navigationService.NavigateTo(new SettingsPage());
            }), DispatcherPriority.ApplicationIdle);
        }

        private void ChangeVolume(double newVolume)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.SetMusicVolume(newVolume);

            Properties.Settings.Default.lastVolume = newVolume;
            Properties.Settings.Default.Save();
        }

        private void ExecuteClose()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new MainMenuPage());
        }
    }
}
