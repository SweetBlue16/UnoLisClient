using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UnoLisClient.UI.Converters
{
    public class BoolToThicknessConverter : IValueConverter
    {
        public Thickness TrueThickness { get; set; } = new Thickness(3);
        public Thickness FalseThickness { get; set; } = new Thickness(1);

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
