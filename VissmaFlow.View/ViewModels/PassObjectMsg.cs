using CommunityToolkit.Mvvm.Messaging.Messages;

namespace VissmaFlow.View.ViewModels
{
    public class PassObjectMsg : ValueChangedMessage<object>
    {
        public PassObjectMsg(object value) : base(value)
        {
        }
    }
}
