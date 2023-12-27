using Avalonia.Controls.ApplicationLifetimes;
using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.View.Dialogs.Events;

public partial class AddEventWindow : DialogWindow
{
    public AddEventWindow()
    {
        InitializeComponent();
    }



    public AddEventWindow(Event ev)
    {
        this.Resources.Add("Event", ev);
        InitializeComponent();        
        
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DataContext = desktop.MainWindow.DataContext;
        }
    }
}