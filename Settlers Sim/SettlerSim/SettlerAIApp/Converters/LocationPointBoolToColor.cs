using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using SettlerSimLib;

namespace SettlerAIApp.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class LocationPointBoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                return null;
            bool IsHarbor = (bool)value;
            if (IsHarbor)
                return Brushes.DarkBlue;
            else
                return Brushes.DarkGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
