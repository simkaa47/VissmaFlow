using Avalonia.Controls;
using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.View.UserControls.Administration;

public partial class EventsViewControl : UserControl
{
    public EventsViewControl()
    {
        InitializeComponent();
        EventsGrid.LoadingRow += (o, e) =>
        {
            if (e.Row.DataContext is Event @event)
                e.Row.IsVisible = !@event.UnVisisble;

        };
    }
}