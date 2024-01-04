using CommunityToolkit.Mvvm.Messaging.Messages;

namespace VissmaFlow.View.ViewModels
{   
    public class OskControlMsg : ValueChangedMessage<bool>
    {
        public OskControlMsg(bool value) : base(value)
        {
        }
    }
}
