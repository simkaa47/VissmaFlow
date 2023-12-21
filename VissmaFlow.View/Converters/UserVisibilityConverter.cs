using System;
using System.Globalization;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.Converters
{
    public class UserVisibilityConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null || (value is not User user)) return false;
            if (parameter is null || parameter is not UserAccessLevel userAccessLevel) return false;
            return user.AccessLevel >= userAccessLevel;
        }
    }
}
