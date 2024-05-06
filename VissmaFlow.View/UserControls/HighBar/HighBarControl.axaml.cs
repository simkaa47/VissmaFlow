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
    

    private async void CloseAppAsync(object? sender, RoutedEventArgs args)
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

    private async Task ExecuteLinuxCmd(string cmd, string description)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return;

        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            var settService = app?.GetService<PcSettingsViewModel>();
            if (settService is not null && desktop.MainWindow is not null && app is not null)
            {
                if (settService.PcSettings?.Password is not null)
                {
                    var questuinService = app.GetService<IQuestionDialog>();
                    if (await questuinService.Ask(description, description))
                    {
                        desktop.MainWindow.Close();
                    }                   
                    ShellHelper.BashCommand($"echo {settService.PcSettings.Password} | sudo {cmd}");
                }
            }
        }
    }


    private async void ShutdownPcAsync(object? sender, RoutedEventArgs args)
    {
        await ExecuteLinuxCmd("shutdown now", "Выключить прибор?");
    }

    private async void RebootPcAsync(object? sender, RoutedEventArgs args)
    {
        await ExecuteLinuxCmd("reboot", "Перезагрузить прибор?");
    }

}