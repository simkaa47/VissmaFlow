using System.Collections.Generic;
using System.Globalization;
using System;
using System.Linq;

namespace VissmaFlow.View.Converters
{
    public class GetIfTrueConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (!(parameter is IEnumerable<object> list)) return null;
            if (list.Count() < 2) return null;
            if (!(value is bool condition)) return null;
            if (condition)
            {
                return list.ElementAt(1);
            }
            else return list.ElementAt(0);
        }

    }
}
