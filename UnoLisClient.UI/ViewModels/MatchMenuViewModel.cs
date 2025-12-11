using System.Windows.Controls;
using System.Windows.Input;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.Views.UnoLisPages;

namespace UnoLisClient.UI.ViewModels
{
    public class MatchMenuViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        
        public ICommand GoBackCommand { get; }
        public ICommand CreateMatchCommand { get; }
        public ICommand JoinMatchCommand { get; }

        public MatchMenuViewModel(Page view, IDialogService dialogService) : base(dialogService)
        {
            _navigationService = (INavigationService)view;

            GoBackCommand = new RelayCommand(ExecuteGoBack);
            CreateMatchCommand = new RelayCommand(ExecuteCreateMatch);
            JoinMatchCommand = new RelayCommand(ExecuteJoinMatch);
        }

        private void ExecuteGoBack()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new MainMenuPage());
        }

        private void ExecuteCreateMatch()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new GameSettingsPage());
        }

        private void ExecuteJoinMatch()
        {
            SoundManager.PlayClick();
            _navigationService.NavigateTo(new JoinMatchPage());
        }
    }
}
