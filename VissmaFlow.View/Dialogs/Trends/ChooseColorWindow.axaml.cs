using Avalonia.Media;

namespace VissmaFlow.View.Dialogs.Trends;

public partial class ChooseColorWindow : DialogWindow
{
    public ChooseColorWindow()
    {
        InitializeComponent();
    }

    public ChooseColorWindow(SolidColorBrush color)
    {
        InitializeComponent();
        this.DataContext = color;
    }
}