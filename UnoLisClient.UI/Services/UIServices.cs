using System.Windows.Controls;

namespace UnoLisClient.UI.Services
{
    public interface INavigationService
    {
        void NavigateTo(Page page);
        void GoBack();
    }

    public interface IDialogService
    {
        void ShowLoading();
        void HideLoading();
        void ShowAlert(string title, string message);
    }
}
