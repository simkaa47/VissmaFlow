using Avalonia.Media;
using System;
using System.Globalization;

namespace VissmaFlow.View.Converters
{
    public class ColorConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(!(value is string ColorString))return null;
            var color = new Avalonia.Media.Color();
            if(Color.TryParse(ColorString, out color))
            {
                return color;
            }
            return null;
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is not null)return value.ToString();
            return "#00000000";
        }
    }
}
