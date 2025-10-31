using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, INavigationService, IDialogService
    {
        private LoadingPopUpWindow _loadingPopUpWindow;

        public LoginPage()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this, this);
        }

        public void NavigateTo(Page page)
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.Navigate(page));
        }

        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.GoBack());
        }

        public void ShowLoading()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_loadingPopUpWindow == null || !_loadingPopUpWindow.IsVisible)
                {
                    _loadingPopUpWindow = new LoadingPopUpWindow
                    {
                        Owner = Window.GetWindow(this)
                    };
                    _loadingPopUpWindow.Show();
                }
            });
        }

        public void HideLoading()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loadingPopUpWindow?.StopLoadingAndClose();
                _loadingPopUpWindow = null;
            });
        }

        public void ShowAlert(string title, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                new SimplePopUpWindow(title, message)
                {
                    Owner = Window.GetWindow(this)
                }
                .ShowDialog();
            });
        }
    }
}
