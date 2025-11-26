using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoLisClient.Logic.Enums;
using UnoLisClient.Logic.Services;
using UnoLisClient.UI.Commands;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.Utilities;
using UnoLisClient.UI.ViewModels.ViewModelEntities;
using UnoLisClient.UI.Views.UnoLisPages;
using UnoLisServer.Common.Enums;

namespace UnoLisClient.UI.ViewModels
{
    public class SelectBackgroundViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMatchmakingService _matchmakingService;
        private readonly string _lobbyCode;

        public ObservableCollection<BackgroundItemViewModel> Backgrounds { get; } = new ObservableCollection<BackgroundItemViewModel>();

        private BackgroundItemViewModel _selectedBackground;
        public BackgroundItemViewModel SelectedBackground
        {
            get => _selectedBackground;
            set
            {
                if (SetProperty(ref _selectedBackground, value))
                {
                    foreach (var background in Backgrounds)
                    {
                        background.IsSelected = (background == value);
                    }
                    (ConfirmCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SelectBackgroundCommand { get; }
        public ICommand ConfirmCommand { get; }

        public SelectBackgroundViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            string lobbyCode)
            : base(dialogService)
        {
            _navigationService = navigationService;
            _matchmakingService = MatchmakingService.Instance;
            _lobbyCode = lobbyCode;

            SelectBackgroundCommand = new RelayCommandGeneric<BackgroundItemViewModel>(ExecuteSelectBackground);

            ConfirmCommand = new RelayCommand(async () => await ExecuteConfirmAsync(), () => SelectedBackground != null && !IsLoading);
        }

        public void LoadBackgrounds()
        {
            try
            {
                Backgrounds.Clear();

                Backgrounds.Add(new BackgroundItemViewModel("FEI", "/Assets/FeiImage.png", "FeiVideo.mp4"));
                Backgrounds.Add(new BackgroundItemViewModel("Anfiteatro", "/Assets/AnfiteatroImage.png", "AnfiteatroVideo.mp4"));
                Backgrounds.Add(new BackgroundItemViewModel("Canchas", "/Assets/CanchasImage.png", "CanchaVideo.mp4"));
                Backgrounds.Add(new BackgroundItemViewModel("Banca", "/Assets/BancaImage.png", "BancaVideo.mp4"));

                if (Backgrounds.Any())
                {
                    SelectedBackground = Backgrounds.First();
                }
                else
                {
                    throw new InvalidOperationException("No background assets were loaded.");
                }
            }
            catch (ArgumentNullException ex)
            {
                HandleException(MessageCode.UnhandledException, "Error: Resource path is null.", ex);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.UnhandledException, "Unexpected error initializing background selection.", ex);
            }
        }

        private void ExecuteSelectBackground(BackgroundItemViewModel item)
        {
            if (item == null) return;
            SoundManager.PlayClick();
            SelectedBackground = item;
        }

        private async Task ExecuteConfirmAsync()
        {
            if (SelectedBackground == null) return;

            IsLoading = true;
            SoundManager.PlayClick();

            try
            {
                bool success = await _matchmakingService.SetLobbyBackgroundAsync(_lobbyCode, SelectedBackground.VideoName);

                if (success)
                {
                    _dialogService.ShowAlert("Partida Creada", $"¡Éxito! Código de sala: {_lobbyCode}", PopUpIconType.Success);
                    _navigationService.NavigateTo(new MatchLobbyPage(_lobbyCode));
                }
                else
                {
                    _dialogService.ShowWarning("Could not set background. Please try again.");
                }
            }
            catch (TimeoutException ex)
            {
                HandleException(MessageCode.Timeout, "Server request timed out.", ex);
            }
            catch (CommunicationException ex)
            {
                HandleException(MessageCode.ConnectionFailed, "Network error communicating with server.", ex);
            }
            catch (Exception ex)
            {
                HandleException(MessageCode.ConfigurationError, "Unexpected error setting background.", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}