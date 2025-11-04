using System.Collections.Generic;
using System.Windows.Controls;

namespace UnoLisClient.UI.Services
{
    public interface IDialogService
    {
        void ShowLoading(Page page);
        void HideLoading();
        void ShowAlert(string title, string message);
        void ShowWarning(string message);
        string HandleValidationErrors(List<string> validationErrors);
        string ShowInputDialog(string title, string message, string placeholder);
        bool ShowQuestionDialog(string title, string question);
    }
}
