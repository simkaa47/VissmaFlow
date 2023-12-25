using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Runtime.InteropServices;

namespace VissmaFlow.View.UserControls.Keyboard;

public partial class CoporateWindow : Window
{
    public static readonly StyledProperty<object> CoporateContentProperty = AvaloniaProperty.Register<CoporateWindow, object>(nameof(CoporateContent));

    public object CoporateContent
    {
        get { return GetValue(CoporateContentProperty); }
        set { SetValue(CoporateContentProperty, value); }
    }

    public CoporateWindow()
    {
        DataContext = this;
        
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
        
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


    private void CloseWindow(object? sender, RoutedEventArgs args)
    {
        Close();
    }

}