using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UnoLisClient.UI.Converters
{
    public class BoolToThicknessConverter : IValueConverter
    {
        private const int TrueThicknessDefault = 3;
        private const int FalseThicknessDefault = 1;

        public Thickness TrueThickness { get; set; } = new Thickness(TrueThicknessDefault);
        public Thickness FalseThickness { get; set; } = new Thickness(FalseThicknessDefault);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueThickness : FalseThickness;
            }
            return FalseThickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
