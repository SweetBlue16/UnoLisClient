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
using System.Collections.ObjectModel;
using System.ServiceModel;
using UnoLisClient.Logic.UnoLisServerReference.Chat;
using UnoLisClient.UI.Utilities;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.Controls
{
    /// <summary>
    /// Its DataContext must be a ChatViewModel
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
        }

        public void SetViewModel(ChatViewModel viewModel)
        {
            this.DataContext = viewModel;
        }
    }
}