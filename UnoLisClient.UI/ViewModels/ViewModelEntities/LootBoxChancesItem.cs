using System.Collections.ObjectModel;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class LootBoxChancesItem : ObservableObject
    {
        private const string RaritySpecial = "special";
        private const string RarityEpic = "epic";
        private const string RarityLegendary = "legendary";

        private const string BrushSpecial = "LightGreen";
        private const string BrushEpic = "DodgerBlue";
        private const string BrushLegendary = "Gold";
        private const string BrushDefault = "#AAAAAA";

        public string BoxName { get; set; }
        public string Rarity { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public ObservableCollection<string> PossibleAvatars { get; set; }
            = new ObservableCollection<string>();

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
