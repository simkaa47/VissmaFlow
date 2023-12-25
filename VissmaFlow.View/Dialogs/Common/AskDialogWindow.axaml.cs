using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace VissmaFlow.View.Dialogs.Common;

public partial class AskDialogWindow : Window
{
    public AskDialogWindow()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
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