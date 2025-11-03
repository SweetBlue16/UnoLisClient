using System.Collections.Generic;
using System.Windows;

namespace UnoLisClient.UI.Services
{
    public interface IDialogService
    {
        void ShowLoading(Window owner);
        void HideLoading();
        void ShowAlert(string title, string message, Window owner);
        void ShowWarning(string message, Window owner);
        void HandleValidationErrors(List<string> validationErrors, Window owner);
    }
}
