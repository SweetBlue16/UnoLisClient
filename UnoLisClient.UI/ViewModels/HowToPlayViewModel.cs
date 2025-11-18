using System.Windows.Input;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.ViewModels
{
    public class HowToPlayViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand BackCommand { get; }

        public HowToPlayViewModel(INavigationService navigationService, IDialogService dialogService)
            : base(dialogService)
        {
            _navigationService = navigationService;
            BackCommand = new RelayCommand(ExecuteBack);
        }

        private void ExecuteBack()
        {
            SoundManager.PlayClick();
            _navigationService.GoBack();
        }
    }
}
