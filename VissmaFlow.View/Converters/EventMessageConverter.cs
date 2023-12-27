using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.View.Converters
{
    public class EventMessageConverter:Converter
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is null || value is not Event @event)
            {
                return string.Empty;
            }
            if(@event.IsActive)
            {
                return string.IsNullOrEmpty(@event.ActiveMessage) ? $"{@event.RtkUnit?.Name} : значение параметра \"{@event.Parameter?.Description}\" {@event.EventCondition} {@event.CompareValue}"
                    : $"{@event.RtkUnit?.Name} : {@event.ActiveMessage}" ;
            }
            else
            {
                return string.IsNullOrEmpty(@event.NonActiveMessage) ? $"{@event.RtkUnit?.Name} : значение параметра \"{@event.Parameter?.Description}\" в допустимых параметрах"
                    : $"{@event.RtkUnit?.Name} : {@event.NonActiveMessage}";
            }
        }
    }
}
