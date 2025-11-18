namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class AvatarModel : ObservableObject
    {
        public int AvatarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public bool IsUnlocked { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string ImagePath
        {
            get
            {
                return $"pack://application:,,,/Avatars/{this.Name}.png";
            }
        }

        public string RarityBrush
        {
            get
            {
                switch (Rarity?.ToLower())
                {
                    case "special": return "LightGreen";
                    case "epic": return "DodgerBlue";
                    case "legendary": return "Gold";
                    default: return "#AAAAAA";
                }
            }
        }
    }
}
