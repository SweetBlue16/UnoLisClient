using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        public static readonly Regex _reportDescriptionRegex = new Regex(@"^[\p{L}\p{M}\p{N}\p{P}\p{S}\s]{1,255}$");

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

        private void StrongReportDescriptionPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !_reportDescriptionRegex.IsMatch(e.Text);
        }
    }
}
