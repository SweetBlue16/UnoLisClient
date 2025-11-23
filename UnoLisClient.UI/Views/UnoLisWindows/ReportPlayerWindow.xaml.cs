using System.Collections.Generic;
using System.Windows;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.UnoLisWindows
{
    /// <summary>
    /// Interaction logic for ReportPlayerWindow.xaml
    /// </summary>
    public partial class ReportPlayerWindow : Window
    {
        public ReportPlayerWindow(List<string> playersList)
        {
            InitializeComponent();
            var viewModel = new ReportPlayerViewModel(new AlertManager(),
                new ReportService(),
                new SessionContext(),
                new ReportTrackerService(),
                playersList,
                null
            );
            Configure(viewModel);
        }

        public void Configure(ReportPlayerViewModel viewModel)
        {
            DataContext = viewModel;
            viewModel.RequestClose += () => Close();
        }
    }
}
