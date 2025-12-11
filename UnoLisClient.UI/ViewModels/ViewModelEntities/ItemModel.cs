using System;
using System.Windows.Input;
using UnoLisClient.UI.Commands;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public enum ItemType { Shield, Thief, ExtraTurn, Swap }

    public class ItemModel : ObservableObject
    {
        private const string ImageBasePath = "pack://application:,,,/Assets/Items/";
        private const string ImageFileSuffix = "Item.png";

        public ItemType Type { get; }
        public string ImagePath => $"{ImageBasePath}{Type}{ImageFileSuffix}";
        public ICommand UseItemCommand { get; }

        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        public ItemModel(ItemType type, int initialCount, Action<ItemType> onUseItem)
        {
            Type = type;
            _count = initialCount;
            UseItemCommand = new RelayCommand(() => onUseItem(Type), () => Count > 0);
        }

        public void UpdateCanExecute()
        {
            (UseItemCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}
