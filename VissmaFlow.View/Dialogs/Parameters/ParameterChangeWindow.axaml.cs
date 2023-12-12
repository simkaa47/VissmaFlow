using Avalonia.Controls;
using Avalonia.Interactivity;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.View.Dialogs.Parameters;

public partial class ParameterChangeWindow : Window
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
}