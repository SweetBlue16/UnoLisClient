using System.Collections.ObjectModel;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class LootBoxChancesItem : BaseViewModel
    {
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
                    case "special": return "LightGreen";
                    case "epic": return "DodgerBlue";
                    case "legendary": return "Gold";
                    default: return "#AAAAAA";
                }
            }
        }
    }
}
