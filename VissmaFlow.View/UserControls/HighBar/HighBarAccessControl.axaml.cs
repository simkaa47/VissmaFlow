using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Net.Http.Headers;
using VissmaFlow.View.Dialogs.AccessControl;

namespace VissmaFlow.View.UserControls.HighBar;

public partial class HighBarAccessControl : UserControl
{
    public HighBarAccessControl()
    {
        InitializeComponent();
    }

    private async void LoginClick(object sender, RoutedEventArgs e)
    {
        if(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var logWindow = new LoginWindow();
            await logWindow.ShowDialogAsync();
        }
    }
}