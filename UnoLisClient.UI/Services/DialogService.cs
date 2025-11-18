using System.Windows;
using UnoLisClient.UI.Views.UnoLisWindows;

namespace UnoLisClient.UI.Services
{
    /// <summary>
    /// Implementación de los servicios de diálogo de la UI.
    /// Implementa ambas interfaces (IDialogService y IModalService) para 
    /// mantener la compatibilidad con código existente y añadir nueva funcionalidad.
    /// </summary>
    public class DialogService : IModalService
    {
        private Window _loadingWindow;
        public void ShowLoading()
        {
            _loadingWindow = new Window
            {
                Width = 250,
                Height = 100,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Background = System.Windows.Media.Brushes.Black,
                Opacity = 0.8,
                Content = new System.Windows.Controls.TextBlock
                {
                    Text = "Loading...",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                },
                Owner = Application.Current.MainWindow
            };

            _loadingWindow.Show();
        }

        public void HideLoading()
        {
            if (_loadingWindow != null)
            {
                _loadingWindow.Close();
                _loadingWindow = null;
            }
        }
        public void ShowAlert(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MessageBoxResult ShowConfirmation(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public string ShowTextInputDialog(string title, string instruction)
        {
            var dialogWindow = new AddFriendWindow();
            bool? result = dialogWindow.ShowDialog();

            if (result == true)
            {
                return dialogWindow.ResultNickname;
            }
            return null;
        }
    }
}