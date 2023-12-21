using Avalonia.Controls;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.View.UserControls.Keyboard;

namespace VissmaFlow.View.Dialogs.Parameters;

public partial class ParameterChangeWindow : DialogWindow
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
   
}