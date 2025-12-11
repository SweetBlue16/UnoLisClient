using System;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.UI.Utilities
{
    public static class CardAssetHelper
    {
        private const string BasePath = "pack://application:,,,/Assets/Cards/";
        private const string WildCardPath = "WildCard.png";
        private const string WildDrawFourCardPath = "WildDrawFourCard.png";
        private const string WildDrawTenCardPath = "WildDrawTenCard.png";
        private const string WildDrawSkipReverseCardPath = "WildDrawSkipReverseCard.png";
        private const string BackCardPath = "BackUNOCard.png";

        public static string GetImagePath(CardColor color, CardValue value)
        {
            try
            {
                string fileName = "";

                if (IsWild(value))
                {
                    switch (value)
                    {
                        case CardValue.Wild:
                            fileName = WildCardPath;
                            break;
                        case CardValue.WildDrawFour:
                            fileName = WildDrawFourCardPath;
                            break;
                        case CardValue.WildDrawTen:
                            fileName = WildDrawTenCardPath;
                            break;
                        case CardValue.WildDrawSkipReverseFour:
                            fileName = WildDrawSkipReverseCardPath;
                            break;
                    }
                }
                else
                {
                    string colorName = color.ToString();
                    string valueName = value.ToString();

                    fileName = $"{valueName}{colorName}Card.png";
                }

                return $"{BasePath}{fileName}";
            }
            catch
            {
                return $"{BasePath}{BackCardPath}";
            }
        }

        private static bool IsWild(CardValue value)
        {
            return value == CardValue.Wild ||
                   value == CardValue.WildDrawFour ||
                   value == CardValue.WildDrawTen ||
                   value == CardValue.WildDrawSkipReverseFour;
        }
    }
}