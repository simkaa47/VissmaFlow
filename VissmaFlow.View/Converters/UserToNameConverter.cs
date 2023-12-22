using System;
using System.Globalization;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.Converters
{
    public class UserToNameConverter:Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null || !(value is User user)) return "Пользователь не авторизован";
            return user.FullName;            
        }
    }
}
