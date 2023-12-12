using System.Globalization;
using System;

namespace VissmaFlow.View.Converters
{
    public class IfNotEqualConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is null) return true;
            if (value is null) return true;
            return value.ToString() != parameter.ToString();
        }
    }
}
