using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UnoLisClient.UI.Properties.Langs;
using UnoLisClient.UI.ViewModels.ViewModelEntities;

namespace UnoLisClient.UI.Views.PopUpWindows
{
    /// <summary>
    /// Interaction logic for MatchResultsPopUpWindow.xaml
    /// </summary>
    public partial class MatchResultsPopUpWindow : Window
    {
        public MatchResultsPopUpWindow(List<MatchResultEntry> results)
        {
            InitializeComponent();
            PopulateDataGrid(results);
            Title = Match.MatchResultsLabel.ToUpper();
        }

        private void ClickOkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void PopulateDataGrid(List<MatchResultEntry> results)
        {
            ObservableCollection<MatchResultEntry> data = new ObservableCollection<MatchResultEntry>(results);
            if (results != null && results.Count > 0)
            {
                ResultsDataGrid.ItemsSource = data;
            }
        }
    }
}
