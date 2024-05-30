using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Styling;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Infrastructure.Helpers;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.ViewModels;
using VissmaFlow.View.Dialogs;
using VissmaFlow.View.Dialogs.AccessControl;
using VissmaFlow.View.Services;

namespace VissmaFlow.View.UserControls;

public partial class PageSelector : DialogWindow
{
    public PageSelector()
    {
        InitializeComponent();
        AffectsMeasure<PageSelector>(ItemsSourceProperty);
        AffectsMeasure<PageSelector>(SelectedItemProperty);
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            if (app != null)
            {
                AccessViewModel = app.GetService<AccessViewModel>();
                if(AccessViewModel is not null)
                {
                    AccessViewModel.PropertyChanged += (o, args) => 
                    { 
                        if(args.PropertyName == nameof(AccessViewModel.CurrentUser))
                        {
                            GetActualTabs();
                        }
                    };
                }
            }
        }  
    }


    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        GetActualTabs();
    }


    private void GetActualTabs()
    {
        if (AccessViewModel is null) return;
        UserAccessLevel level = AccessViewModel.CurrentUser is null ? UserAccessLevel.None : AccessViewModel.CurrentUser.AccessLevel;
        if (Tabs is null || !(Tabs is List<UserAccessControl> controls)) return;
        ItemsSource = controls.Where(c=>level>=c.UserLevel).
            Select(c=>c.Tag).ToList();
        if(SelectedItem is null && Tab is not null && Tab is UserControl control)
        {
            if (ItemsSource is List<object> list)
                SelectedItem = list.Where(i => i == control.Tag).FirstOrDefault();
        }
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

    private async  void ChangeTheme(object? sender, RoutedEventArgs args)
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var app = App.Current as App;
            var service = app?.GetService<ThemeService>();
            if(service != null)
            {
                await service?.ChangeThemeAsync();
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

    private async void LoginClick(object sender, PointerReleasedEventArgs args)
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var logWindow = new LoginWindow();
            await logWindow.ShowDialogAsync();
            GetActualTabs();
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

    private AccessViewModel? AccessViewModel { get; set; }



    public object? Tabs { get; set; }

    public object? Tab { get; set; }


    #region Источник данных
    public object ItemsSource
    {
        get { return (object)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object> ItemsSourceProperty =
        AvaloniaProperty.Register<PageSelector, object>(nameof(ItemsSource), new List<string> { });
    #endregion

    #region Выбранное значение
    public object? SelectedItem
    {
        get { return (object?)GetValue(SelectedItemProperty); }
        set
        {
            SetValue(SelectedItemProperty, value);   
        }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<PageSelector, object?>(nameof(SelectedItem), null);
    #endregion

    private void OnSelectionChanged(object sender, PointerReleasedEventArgs args)
    {
        if(SelectedItem is not null && SelectedItem is string str)
        {
            if (Tabs is not null && Tabs is List<UserAccessControl> controls)
            {
                Tab = controls.Where(c => c.Tag is string tag && tag == str).FirstOrDefault();
                needToCloseDialog = true;
            }
        }
    }







}