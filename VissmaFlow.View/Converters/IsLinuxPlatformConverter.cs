using System.Globalization;
using System.Runtime.InteropServices;
using System;

namespace VissmaFlow.View.Converters
{
    public class IsLinuxPlatformConverter:Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
