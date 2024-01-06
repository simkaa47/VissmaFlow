using Avalonia.Controls;
using VissmaFlow.View.ViewModels;

namespace VissmaFlow.View.UserControls.Main;

public partial class MainTrendsControl : UserControl
{
    public MainTrendsControl()
    {
        InitializeComponent();
        if (App.Current is App app)
        {
            var trendsVm = app.GetService<TrendsViewModel>();
            this.DataContext = trendsVm;
        }
    }
}