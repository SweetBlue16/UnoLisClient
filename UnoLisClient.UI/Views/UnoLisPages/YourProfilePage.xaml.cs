using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisClient.UI.Utilities;
using UnoLisServer.Common.Models;
using UnoLisClient.UI.Views.PopUpWindows;
using UnoLisClient.Logic.Mappers;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.Models;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for YourProfilePage.xaml
    /// </summary>
    public partial class YourProfilePage : Page, INavigationService
    {
        public YourProfilePage()
        {
            InitializeComponent();
            DataContext = new ProfileViewViewModel(this, new AlertManager());
        }

        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.GoBack()
            );
        }

        public void NavigateTo(Page page)
        {
            Application.Current.Dispatcher.Invoke(() =>
                NavigationService?.Navigate(page)
            );
        }

        private void YourProfilePageLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProfileViewViewModel viewModel)
            {
                if (viewModel.LoadProfileCommand.CanExecute(null))
                {
                    viewModel.LoadProfileCommand.Execute(null);
                }
            }
        }
    }
}
