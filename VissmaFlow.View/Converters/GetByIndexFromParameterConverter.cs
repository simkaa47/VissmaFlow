using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VissmaFlow.View.Converters
{
    internal class GetByIndexFromParameterConverter : Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null || parameter is null) return null;
            int index = 0;
            if (!int.TryParse(value.ToString(), out index))
            {
                if(!TryCastToInt(value, out index)) return null;
            }
            if (!(parameter is IEnumerable<object> list)) return null;
            if (list.Count() < index + 1) return null;
            return list.ElementAt(index);
        }

        private  bool TryCastToInt(object? obj, out int result)
        {
            if (obj is null)
            {
                result = 0;
                return false;
            }
            try
            {
                result = (int)obj;
                return true;
            }

            catch
            {
                result = 0;
                return false;
            }
        }

    }
}
