using System.Text.RegularExpressions;
using System.Windows.Controls;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.Views.Controls
{
    /// <summary>
    /// Its DataContext must be a ChatViewModel
    /// </summary>
    public partial class ChatControl : UserControl
    {
        private static readonly Regex _chatMessageRegex = new Regex(@"^[\p{L}\p{M}\p{N}\p{P}\p{S}\s]{1,255}$");

        public ChatControl()
        {
            InitializeComponent();
        }

        public void SetViewModel(ChatViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        private void StrongMessagePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !_chatMessageRegex.IsMatch(e.Text);
        }
    }
}