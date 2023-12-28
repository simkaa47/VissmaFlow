using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VissmaFlow.Core.Models.Event;

namespace VissmaFlow.View.UserControls.Events;

public partial class CurrentErrors : UserControl
{
    public CurrentErrors()
    {
        InitializeComponent();
        //EventsGrid.LoadingRow += (o, e) =>
        //{
        //    if (e.Row.DataContext is Event @event)
        //        e.Row.IsVisible = @event.IsActive;

        //};
    }
}