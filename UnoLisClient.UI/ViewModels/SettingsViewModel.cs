using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
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
        private const string EnglishCode = "en-US";
        private const string SpanishCode = "es-MX";
        private const int DefaultVolume = 50;

        private readonly INavigationService _navigationService;
        private readonly Page _view;
        private bool _languageChanged = false;

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
                new LanguageItem { Name = Settings.EnglishComboItem, Code = EnglishCode },
                new LanguageItem { Name = Settings.SpanishComboItem, Code = SpanishCode }
            };

            var savedLang = Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(savedLang))
            {
                savedLang = EnglishCode;
            }

            _selectedLanguage = AvailableLanguages.FirstOrDefault(l => l.Code == savedLang) ?? AvailableLanguages[0];
            OnPropertyChanged(nameof(SelectedLanguage));

            double savedVol = Properties.Settings.Default.lastVolume;
            _volume = savedVol > 0 ? savedVol : DefaultVolume;
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

            _languageChanged = true;

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

            var mainWindow = Application.Current.MainWindow as MainWindow;
            var frame = mainWindow?.MainFrame;

            if (!_languageChanged && frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                return;
            }

            if (frame != null && frame.BackStack.Cast<JournalEntry>().Any())
            {
                var previousEntry = frame.BackStack.Cast<JournalEntry>()
                    .LastOrDefault(entry => !entry.Source.ToString().Contains("SettingsPage"));

                if (previousEntry != null)
                {
                    string lastPageSource = previousEntry.Source.ToString();

                    if (lastPageSource.Contains("MainMenuPage"))
                    {
                        _navigationService.NavigateTo(new MainMenuPage());
                    }
                    else if (lastPageSource.Contains("GamePage")) // 
                    {
                        _navigationService.NavigateTo(new GamePage());
                    }
                    else if (lastPageSource.Contains("JoinMatchPage"))
                    {
                        _navigationService.NavigateTo(new JoinMatchPage());
                    }
                    else if (lastPageSource.Contains("MatchBoardPage"))
                    {
                        frame.GoBack();
                    }
                    else
                    {
                        _navigationService.NavigateTo(new MainMenuPage());
                    }
                }
                else
                {
                    _navigationService.NavigateTo(new MainMenuPage());
                }
            }
            else
            {
                _navigationService.NavigateTo(new MainMenuPage());
            }
        }
    }
}