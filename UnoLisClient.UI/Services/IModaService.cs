using System.Windows;

namespace UnoLisClient.UI.Services
{
    /// <summary>
    /// Segregated interface (SRP) for dialogs that require user input
    /// Yes/No confirmations or text input.
    /// </summary>
    public interface IModalService
    {
        MessageBoxResult ShowConfirmation(string title, string message);
        string ShowTextInputDialog(string title, string instruction);
        void ShowAlert(string title, string message);
    }
}