using System;
using System.Globalization;

namespace VissmaFlow.View.Converters
{
    internal class RowVisibilityConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
