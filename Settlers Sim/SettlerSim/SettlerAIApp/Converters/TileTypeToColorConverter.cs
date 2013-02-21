using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using SettlerSimLib;

namespace SettlerAIApp.Converters
{
    [ValueConversion(typeof(LandType), typeof(Brush))]
    public class TileTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                return null;
            LandType landType = (LandType)value;
            switch(landType)
            {
                case LandType.Clay:
                    return Brushes.Crimson;
                case LandType.Rock:
                    return Brushes.Gray;
                case LandType.Sand:
                    return Brushes.Yellow;
                case LandType.Sheep:
                    return Brushes.WhiteSmoke;
                case LandType.Wheat:
                    return Brushes.Wheat;
                case LandType.Wood:
                    return Brushes.ForestGreen;
                default:
                    return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
