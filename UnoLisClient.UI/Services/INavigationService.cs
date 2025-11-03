using System.Windows.Controls;

namespace UnoLisClient.UI.Services
{
    public interface INavigationService
    {
        void NavigateTo(Page page);
        void GoBack();
    }
}
