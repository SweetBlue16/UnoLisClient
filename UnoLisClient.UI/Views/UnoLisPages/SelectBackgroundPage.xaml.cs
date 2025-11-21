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
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;
using UnoLisClient.UI.Views.UnoLisWindows;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Lógica de interacción para SelectBackgroundPage.xaml
    /// </summary>
    public partial class SelectBackgroundPage : Page, INavigationService
    {
        private readonly SelectBackgroundViewModel _viewModel;

        public SelectBackgroundPage(string lobbyCode)
        {
            InitializeComponent();

            // 1. Instanciamos el ViewModel inyectando dependencias y el código de sala
            _viewModel = new SelectBackgroundViewModel(
                this,
                new AlertManager(),
                lobbyCode
            );

            // 2. Asignamos el DataContext para que el XAML pueda hacer Binding
            this.DataContext = _viewModel;

            // 3. Llamamos explícitamente a la carga de datos
            // Esto separa la construcción de la lógica de carga (mejor manejo de errores)
            _viewModel.LoadBackgrounds();
        }

        // --- Implementación de INavigationService ---

        public void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }

        public void GoBack()
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
