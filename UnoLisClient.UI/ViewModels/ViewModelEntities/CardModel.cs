using System;
using System.Windows.Input;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;
using UnoLisClient.UI.Commands;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class CardModel : ObservableObject
    {
        public Card CardData { get; }

        public string ImagePath => CardData.ImagePath ?? "pack://application:,,,/Assets/Cards/DrawFour.png";

        private bool _isPlayable;
        public bool IsPlayable
        {
            get => _isPlayable;
            set => SetProperty(ref _isPlayable, value);
        }

        public ICommand PlayCardCommand { get; }

        public CardModel(Card cardData, Action<CardModel> onPlayCard)
        {
            CardData = cardData;
            PlayCardCommand = new RelayCommand(() => onPlayCard(this), () => IsPlayable);
        }

        public void UpdateCanExecute()
        {
            (PlayCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}
