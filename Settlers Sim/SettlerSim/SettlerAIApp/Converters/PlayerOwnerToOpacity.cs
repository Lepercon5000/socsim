using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SettlerAIApp.Converters
{
    [ValueConversion(typeof(int), typeof(double))]
    public class PlayerOwnerToOpacity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(double))
                return null;
            int playerOwnerValue = (int)value;
            if (playerOwnerValue == 0)
                return 0.0;
            else
                return 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
