using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using VissmaFlow.Core.Contracts.Common;

namespace VissmaFlow.View.UserControls.HighBar;

public partial class HighBarControl : UserControl
{
    public HighBarControl()
    {
        InitializeComponent();
    }

    private async void CloseApp(object? sender, RoutedEventArgs args)
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            if (app != null) 
            {
                var questuinService = app.GetService<IQuestionDialog>();
                if(await questuinService.Ask("Закрыть приложение?", "Закрыть приложение?"))
                {
                    desktop.MainWindow.Close();
                }
            
            }
        }
    }
}