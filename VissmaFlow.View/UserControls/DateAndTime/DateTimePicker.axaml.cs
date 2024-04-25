using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Windows.Input;

namespace VissmaFlow.View.UserControls.DateAndTime;

public partial class DateTimePicker : UserControl
{
    public DateTimePicker()
    {
        InitializeComponent();
        InitializeComponent();
        sabri_ = this.FindControl<Button>("sabri");
        pop = this.FindControl<Popup>("Popup");
        popPresenter = this.FindControl<FBCDateTimePickerPresenter>("PopPresenter");
        popPresenter!.Confirmed += PopPresenter_Confirmed;
        popPresenter.Dismissed += PopPresenter_Dismissed;
        sabri.Tapped += Sabri_Tapped;
        AffectsMeasure<DateTimePicker>(DateTimeProperty);
        AffectsMeasure<DateTimePicker>(CommandParameterProperty);
        AffectsMeasure<DateTimePicker>(CommandProperty);
        AffectsMeasure<DateTimePicker>(DescriptionProperty);
    }

    Button? sabri_;
    Popup? pop;
    FBCDateTimePickerPresenter? popPresenter;

    private void PopPresenter_Dismissed(object? sender, System.EventArgs e)
    {
        pop!.Close();

    }

    private void PopPresenter_Confirmed(object? sender, System.EventArgs e)
    {
        DateTime = popPresenter!.Value;
        pop?.Close();
        if (Command is not null)
        {
            Command.Execute(CommandParameter);
        }

    }

    private void Sabri_Tapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        pop!.IsOpen = true;

    }

    #region dt
    public DateTime DateTime
    {
        get { return (DateTime)GetValue(DateTimeProperty); }
        set { SetValue(DateTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<DateTime> DateTimeProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTime>(nameof(DateTime));
    #endregion

    #region Command
    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<DateTimePicker, ICommand>(nameof(Command));
    #endregion

    #region CommandParameter
    public object CommandParameter
    {
        get { return GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<DateTimePicker, object>(nameof(CommandParameter));
    #endregion

    #region Description
    public string Description
    {
        get { return (string)GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<DateTimePicker, string>(nameof(Description));
    #endregion
}