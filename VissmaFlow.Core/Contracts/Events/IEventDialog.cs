using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.Core.Contracts.Events
{
    public interface IEventDialog
    {
        Task<Event?> ShowDialog(Event e);
    }
}
