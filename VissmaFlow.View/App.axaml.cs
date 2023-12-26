using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VissmaFlow.Core.ViewModels;
using VissmaFlow.Core;
using VissmaFlow.Core.Contracts.Parameters;
using VissmaFlow.View.Dialogs.Parameters;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.View.Dialogs.Common;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.View.Dialogs.Communication;
using VissmaFlow.View.UserControls.Keyboard.Layout;
using VissmaFlow.View.UserControls.Keyboard;
using VissmaFlow.View.Dialogs.AccessControl;
using VissmaFlow.Core.Contracts.AccessControl;
using VissmaFlow.Core.Contracts.Events;
using VissmaFlow.View.Dialogs.Events;

namespace VissmaFlow.View
{
    public partial class App : Application
    {
        public T GetService<T>() where T : notnull
        {
            T servise = _host.Services.GetRequiredService<T>();
            return servise;
        }

        private readonly IHost _host;
        public App()
        {
            VirtualKeyboard.AddLayout<VirtualKeyboardLayoutRU>();
            VirtualKeyboard.AddLayout<VirtualKeyboardLayoutUS>();
            VirtualKeyboard.SetDefaultLayout(() => typeof(VirtualKeyboardLayoutUS));
            _host = Host.CreateDefaultBuilder().
                ConfigureServices(services => 
                {
                    services.AddApplicationServices();
                    services.AddTransient<IParameterDialogService, ParameterDialogService>();
                    services.AddTransient<IQuestionDialog, AskDialog>();
                    services.AddTransient<IRtkUnitDialog, AddRtkUnitDialog>();
                    services.AddTransient<IAccessDialogService, UserDialogService>();
                    services.AddTransient<IEventDialog, EventDialog>();
                }).Build();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = GetService<MainViewModel>();

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}