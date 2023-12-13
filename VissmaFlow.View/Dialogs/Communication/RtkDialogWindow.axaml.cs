using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using VissmaFlow.Core.Models.Communication;

namespace VissmaFlow.View.Dialogs.Communication;

public partial class RtkDialogWindow : Window
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
}