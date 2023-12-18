using Avalonia.Markup.Xaml;

namespace VissmaFlow.View.UserControls.Keyboard.Layout;

public partial class VirtualKeyboardLayoutRU : KeyboardLayout
{
    public VirtualKeyboardLayoutRU()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public string LayoutName => "ru-RU";
}