using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.View.UserControls.Keyboard;

namespace VissmaFlow.View.Dialogs.Communication;

public partial class RtkDialogWindow : DialogWindow
{
    public RtkDialogWindow()
    {
        InitializeComponent();
    }

    public RtkDialogWindow(RtkUnit rtk)
    {
        this.DataContext = rtk;
        InitializeComponent();
    }
   
}