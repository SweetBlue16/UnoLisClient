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
using System.Windows.Shapes;

namespace UnoLisClient.UI.Views.UnoLisWindows
{
    /// <summary>
    /// Ventana contenedora (Wrapper) para el UserControl AddFriendModal.
    /// Maneja los eventos del control para cerrar la ventana y devolver un DialogResult.
    /// </summary>
    public partial class AddFriendWindow : Window
    {
        public string ResultNickname { get; private set; }

        public AddFriendWindow()
        {
            InitializeComponent();

            ModalControl.FriendAdded += OnFriendAdded;
            ModalControl.Canceled += OnCanceled;
        }
        private void OnFriendAdded(string nickname)
        {
            this.ResultNickname = nickname;
            this.DialogResult = true;

            this.Close();
        }
        private void OnCanceled()
        {
            this.DialogResult = false;

            this.Close();
        }
    }
}
