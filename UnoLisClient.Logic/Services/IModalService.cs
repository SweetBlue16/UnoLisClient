using System.Windows; 

namespace UnoLisClient.UI.Services
{
    /// <summary>
    /// Interfaz segregada (SRP) para diálogos que requieren input del usuario
    /// (Confirmaciones Sí/No o entrada de texto).
    /// </summary>
    public interface IModalService
    {
        MessageBoxResult ShowConfirmation(string title, string message);
        string ShowTextInputDialog(string title, string instruction);
        void ShowAlert(string title, string message);
    }
}