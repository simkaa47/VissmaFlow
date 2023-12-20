using Avalonia.Controls;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.View.UserControls.Keyboard;

namespace VissmaFlow.View.Dialogs.Parameters;

public partial class ParameterChangeWindow : Window, ITextInputMethodRoot
{
    public ParameterChangeWindow()
    {
        InitializeComponent();
    }

    public ParameterChangeWindow(ParameterBase parameter)
    {
        InitializeComponent();
        this.DataContext = parameter;
    }

    public bool DialogResult { get; set; }
    void Accept_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private VirtualKeyboardTextInputMethod virtualKeyboardTextInput = new VirtualKeyboardTextInputMethod();
    ITextInputMethodImpl ITextInputMethodRoot.InputMethod
    {
        get
        {
            return virtualKeyboardTextInput;
        }
    }
}