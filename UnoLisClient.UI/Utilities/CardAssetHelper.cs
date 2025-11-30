using System;
using UnoLisClient.Logic.UnoLisServerReference.Gameplay;

namespace UnoLisClient.UI.Utilities
{
    public static class CardAssetHelper
    {
        private const string BasePath = "pack://application:,,,/Assets/Cards/";

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
                            fileName = "WildCard.png";
                            break;
                        case CardValue.WildDrawFour:
                            fileName = "WildDrawFourCard.png";
                            break;
                        case CardValue.WildDrawTen:
                            fileName = "WildDrawTenCard.png";
                            break;
                        case CardValue.WildDrawSkipReverseFour:
                            fileName = "WildDrawSkipReverseCard.png";
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
                return $"{BasePath}BackUNOCard.png";
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