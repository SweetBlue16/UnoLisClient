using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UnoLisClient.UI.Utilities
{
    public class ProgressBarWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4 ||
                values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue ||
                values[3] == DependencyProperty.UnsetValue)
            {
                return 0;
            }

            double value = (double)values[0];
            double minimum = (double)values[1];
            double maximum = (double)values[2];
            double actualWidth = (double)values[3];

            if (maximum == minimum || actualWidth == 0)
            {
                return 0;
            }

            double ratio = (value - minimum) / (maximum - minimum);
            return Math.Max(0, ratio * actualWidth);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { DependencyProperty.UnsetValue };
        }
    }
}
