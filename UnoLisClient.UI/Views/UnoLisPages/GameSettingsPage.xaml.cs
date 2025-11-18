using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.PopUpWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for GameSettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page, INavigationService
    {
        private readonly GameSettingsViewModel _viewModel;
        public GameSettingsPage()
        {
            InitializeComponent();

            _viewModel = new GameSettingsViewModel(
                this,
                new AlertManager(),
                MatchmakingService.Instance
                );

            this.DataContext = _viewModel;
        }

        public void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }

        public void GoBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void CheckedMusicToggle(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🎵"; 
        }

        private void UncheckedMusicToggle(object sender, RoutedEventArgs e)
        {
            if (MusicToggle.Content is TextBlock tb)
                tb.Text = "🔇";
            // Aquí: pausar/detener música
            // NOTA: Mejor silenciarla que detenerla para evitar retrasos al reanudar
            //TODO: Implementar la funcionalidad
        }

        private void CheckedSoundToggle(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔊";
        }

        private void UncheckedSoundToggle(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.Content is TextBlock tb)
                tb.Text = "🔈";

        }
    }
}
