using System.Collections.Generic;
using System.Windows.Controls;
using UnoLisClient.Logic.Enums;

namespace UnoLisClient.UI.Services
{
    public interface IDialogService
    {
        void ShowLoading(Page page);
        void HideLoading();
        void ShowAlert(string title, string message, PopUpIconType icon = PopUpIconType.None);
        void ShowWarning(string message);
        string HandleValidationErrors(List<string> validationErrors);
        string ShowInputDialog(string title, string message, string placeholder, PopUpIconType icon = PopUpIconType.None);
        bool ShowQuestionDialog(string title, string question, PopUpIconType icon = PopUpIconType.None);
    }
}
