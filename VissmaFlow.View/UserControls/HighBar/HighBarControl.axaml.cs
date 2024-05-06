using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Infrastructure.Helpers;
using VissmaFlow.Core.Models.Administration;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.View.UserControls.HighBar;

public partial class HighBarControl : UserControl
{
    public HighBarControl()
    {
        InitializeComponent();
    }

    private async void CloseApp(object? sender, RoutedEventArgs args)
    {
        if (OperatingSystem.IsLinux())
            await ShutdownPc();
        else await CloseAppAsync();
    }



    private async Task CloseAppAsync()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            if (app != null && desktop.MainWindow is not null)
            {
                var questuinService = app.GetService<IQuestionDialog>();
                if (await questuinService.Ask("Закрыть приложение?", "Закрыть приложение?"))
                {
                    desktop.MainWindow.Close();
                }
            }
        }
    }

    private async Task ShutdownPc()
    {        
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            var settService = app?.GetService<PcSettingsViewModel>();
            if(settService is not null && desktop.MainWindow is not null && app is not null)
            {
                    if (settService.PcSettings?.Password is not null)
                    {
                        var questuinService = app.GetService<IQuestionDialog>();
                        if (await questuinService.Ask("Выключить прибор?", "Выключить прибор?"))
                        {
                            desktop.MainWindow.Close();
                        }
                        string cmd = $"echo {settService.PcSettings.Password} | sudo shutdown now";
                            ShellHelper.BashCommand(cmd);
                    }
            }
        }
    }

}