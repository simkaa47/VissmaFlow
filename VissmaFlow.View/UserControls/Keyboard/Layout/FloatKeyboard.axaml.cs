using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace VissmaFlow.View.UserControls.Keyboard.Layout;

public partial class FloatKeyboard :  KeyboardLayout
{
    public FloatKeyboard()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        KeyboardInputType = KeyboardInputType.Float;    
    }

    public string LayoutName => "Float";

}