namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class AvatarModel : ObservableObject
    {
        private const string ImageBasePath = "pack://application:,,,/Avatars/";
        private const string ImageExtension = ".png";

        private const string RaritySpecial = "special";
        private const string RarityEpic = "epic";
        private const string RarityLegendary = "legendary";

        private const string BrushSpecial = "LightGreen";
        private const string BrushEpic = "DodgerBlue";
        private const string BrushLegendary = "Gold";
        private const string BrushDefault = "#AAAAAA";

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
                return $"{ImageBasePath}{Name}{ImageExtension}";
            }
        }

        public string RarityBrush
        {
            get
            {
                switch (Rarity?.ToLower())
                {
                    case RaritySpecial: return BrushSpecial;
                    case RarityEpic: return BrushEpic;
                    case RarityLegendary: return BrushLegendary;
                    default: return BrushDefault;
                }
            }
        }
    }
}
