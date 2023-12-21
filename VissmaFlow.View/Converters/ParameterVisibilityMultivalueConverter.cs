using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.View.Converters
{
    public class ParameterVisibilityMultivalueConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count < 2) return false;
            if (!(values[0] is ParameterBase par)) return false;
            if (!(values[1] is User user)) return false;
            return (user.AccessLevel >= par.UserAccessLevel) && !par.IsReadOnly;
        }
    }
}
