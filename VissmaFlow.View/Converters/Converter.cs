using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace VissmaFlow.View.Converters
{
    public class Converter : IValueConverter
    {
        public virtual object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
