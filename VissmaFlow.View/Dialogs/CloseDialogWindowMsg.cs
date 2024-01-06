using CommunityToolkit.Mvvm.Messaging.Messages;

namespace VissmaFlow.View.Dialogs
{
    public class CloseDialogWindowMsg : ValueChangedMessage<bool>
    {
        public CloseDialogWindowMsg(bool value) : base(value)
        {
        }
    }
}
