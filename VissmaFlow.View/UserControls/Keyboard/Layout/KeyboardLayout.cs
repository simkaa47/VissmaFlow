using Avalonia.Controls;

namespace VissmaFlow.View.UserControls.Keyboard.Layout
{
    public abstract class KeyboardLayout : UserControl
    {
        string LayoutName { get; } = string.Empty;
        public KeyboardInputType KeyboardInputType { get; set; }
    }

    public enum KeyboardInputType
    {
        Text,
        Float,
        Decimal
    }
}
