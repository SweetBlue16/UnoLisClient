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

namespace UnoLisClient.UI.Views.Controls
{
    /// <summary>
    /// Interaction logic for AddFriendModal.xaml
    /// </summary>
    public partial class AddFriendModal : UserControl
    {
        public event Action<string> FriendAdded;
        public event Action Canceled;

        public AddFriendModal()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var nickname = NicknameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                MessageBox.Show("Please enter a nickname.", "UNO LIS",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            FriendAdded?.Invoke(nickname);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Canceled?.Invoke();
        }
    }
}
