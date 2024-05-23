using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Infrastructure.Helpers;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.View.UserControls.HighBar;

public partial class HighBarControl : UserControl
{
    public HighBarControl()
    {
        InitializeComponent();
        AffectsMeasure<HighBarControl>(ItemsSourceProperty);
        AffectsMeasure<HighBarControl>(SelectedItemProperty);
        
    }

    PageSelector _pageSelector = new PageSelector();

    private async void OpenSelectorAsync(object? sender, RoutedEventArgs args)
    {
        _pageSelector.Tabs = ItemsSource;
        _pageSelector.Tab = SelectedItem;
        await _pageSelector.ShowDialogAsync();
        if (_pageSelector.Tab is not null)
            SelectedItem = _pageSelector.Tab;
       
    }    

    #region Источник данных
    public object ItemsSource
    {
        get { return (object)GetValue(ItemsSourceProperty); }
        set 
        { 
            if(value is List<UserAccessControl> controls && controls.Count>0)
            {
                SelectedItem = controls[0];
            }
            SetValue(ItemsSourceProperty, value); 
        }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object> ItemsSourceProperty =
        AvaloniaProperty.Register<HighBarControl, object>(nameof(ItemsSource), new List<string> { });
    #endregion

    #region Выбранное значение
    public object SelectedItem
    {
        get { return (object)GetValue(SelectedItemProperty); }
        set
        {
            if (value is not null)
                SetValue(SelectedItemProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object> SelectedItemProperty =
        AvaloniaProperty.Register<HighBarControl, object>(nameof(SelectedItem));
    #endregion

}