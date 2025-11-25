using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace UnoLisClient.UI.Converters
{
    public class RankToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int rank = (int)value;
            string image;

            switch (rank)
            {
                case 1:
                    image = "pack://application:,,,/Assets/Other/1stPlace.png";
                    break;

                case 2:
                    image = "pack://application:,,,/Assets/Other/2ndPlace.png";
                    break;

                case 3:
                    image = "pack://application:,,,/Assets/Other/3rdPlace.png";
                    break;

                default:
                    image = "pack://application:,,,/Assets/Other/4thPlace.png";
                    break;
            }
            return new BitmapImage(new Uri(image));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }
    }
}
