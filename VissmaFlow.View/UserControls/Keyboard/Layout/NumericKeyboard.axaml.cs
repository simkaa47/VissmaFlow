using Avalonia.Markup.Xaml;

namespace VissmaFlow.View.UserControls.Keyboard.Layout;

public partial class NumericKeyboard : KeyboardLayout
{
    public NumericKeyboard()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        KeyboardInputType = KeyboardInputType.Decimal;
    }

    public string LayoutName => "Int";
}