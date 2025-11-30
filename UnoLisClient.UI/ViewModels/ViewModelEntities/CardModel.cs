using System;
using System.Windows.Input;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Commands;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class CardModel : ObservableObject 
    {
        public Card CardData { get; }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        private bool _isPlayable;
        public bool IsPlayable
        {
            get => _isPlayable;
            set
            {
                if (SetProperty(ref _isPlayable, value))
                {
                    UpdateCanExecute();
                }
            }
        }

        public ICommand PlayCardCommand { get; }
        private readonly Action<CardModel> _playAction;

        public CardModel(Card cardData, Action<CardModel> playAction)
        {
            CardData = cardData;
            _playAction = playAction;
            PlayCardCommand = new RelayCommand(ExecutePlay, CanPlay);
            IsPlayable = false;
        }

        private void ExecutePlay()
        {
            _playAction?.Invoke(this);
        }

        private bool CanPlay()
        {
            return IsPlayable;
        }

        public void UpdateCanExecute()
        {
            (PlayCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}