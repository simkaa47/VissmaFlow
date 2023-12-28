using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using VissmaFlow.Core.Models.Trends;
using VissmaFlow.View.Dialogs.Trends;

namespace VissmaFlow.View.UserControls.Administration;

public partial class CurveSettingsControl : UserControl
{
    public CurveSettingsControl()
    {
        InitializeComponent();
    }




    [RelayCommand]
    private async Task ChooseColor(object o)
    {
        if (!(o is SolidColorBrush brush)) return;
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var dialogBrush = new SolidColorBrush(brush.Color);
            var dialog = new ChooseColorWindow(dialogBrush);
            await dialog.ShowDialog(desktop.MainWindow);
            if(dialog.DialogResult)
                brush.Color = dialogBrush.Color;

            
        }
    }
}