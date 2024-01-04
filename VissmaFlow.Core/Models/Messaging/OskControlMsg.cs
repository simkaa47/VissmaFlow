using CommunityToolkit.Mvvm.Messaging.Messages;

namespace VissmaFlow.Core.Models.Messaging
{
    public class PassObjectMsg : ValueChangedMessage<object>
    {
        public PassObjectMsg(object value) : base(value)
        {
        }
    }
}
