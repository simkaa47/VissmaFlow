using System;
using System.Globalization;

namespace VissmaFlow.View.Converters
{
    public class DivideConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null || parameter is null) return 0;
            if (!(float.TryParse(value.ToString(), out float x))) return 0;
            if (!(float.TryParse(parameter.ToString(), out float k))) return 0;
            return x / k;
        }
    }
}
