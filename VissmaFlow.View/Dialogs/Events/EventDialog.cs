using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Events;
using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.View.Dialogs.Events
{
    public class EventDialog : IEventDialog
    {
        public async Task<Event?> ShowDialog(Event ev)
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return null;
            }            
            AddEventWindow window = new AddEventWindow(ev);
            await window.ShowDialog(desktop.MainWindow);
            if (window.DialogResult) return ev;
            return null;
        }
    }
}
