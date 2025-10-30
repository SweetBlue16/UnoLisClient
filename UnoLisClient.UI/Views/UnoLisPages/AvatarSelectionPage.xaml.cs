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

namespace UnoLisClient.UI.Views.UnoLisPages
{
    /// <summary>
    /// Interaction logic for AvatarSelectionPage.xaml
    /// </summary>
    public partial class AvatarSelectionPage : Page
    {
        public AvatarSelectionPage()
        {
            InitializeComponent();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Avatar saved successfully!");
        }

        private void ClickCancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
