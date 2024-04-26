using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;

namespace VissmaFlow.View.UserControls.DateAndTime
{
    public class FBCDateTimePickerPresenter : PickerPresenterBase
    {
        public FBCDateTimePickerPresenter() : base()
        {
            Value = DateTime.Now;
        }

        public static readonly DirectProperty<FBCDateTimePickerPresenter, DateTime> DateOnlyProperty =
            AvaloniaProperty.RegisterDirect<FBCDateTimePickerPresenter, DateTime>(nameof(DateOnly),
                x => x.DateOnly, (x, v) => x.DateOnly = v);

        public static readonly DirectProperty<FBCDateTimePickerPresenter, decimal> HourProperty =
            AvaloniaProperty.RegisterDirect<FBCDateTimePickerPresenter, decimal>(nameof(Hour),
                x => x.Hour, (x, v) => x.Hour = v);

        public static readonly DirectProperty<FBCDateTimePickerPresenter, decimal> MinuteProperty =
            AvaloniaProperty.RegisterDirect<FBCDateTimePickerPresenter, decimal>(nameof(Minute),
                x => x.Minute, (x, v) => x.Minute = v);

        public static readonly DirectProperty<FBCDateTimePickerPresenter, decimal> SecondProperty =
            AvaloniaProperty.RegisterDirect<FBCDateTimePickerPresenter, decimal>(nameof(Second),
                x => x.Second, (x, v) => x.Second = v);


        private DateTime dateOnly;
        private decimal hour;
        private decimal minute;
        private decimal second;

        public DateTime DateOnly
        {
            get { return dateOnly; }
            set
            {
                SetAndRaise(DateOnlyProperty, ref dateOnly, value);
            }
        }

        public decimal Hour
        {
            get { return hour; }
            set
            {
                SetAndRaise(HourProperty, ref hour, value);
            }
        }
        public decimal Minute
        {
            get { return minute; }
            set
            {
                minute = value;
                SetAndRaise(MinuteProperty, ref minute, value);
            }
        }
        public decimal Second
        {
            get { return second; }
            set
            {
                second = value;
                SetAndRaise(SecondProperty, ref second, value);
            }
        }
        NumericUpDown? nuHour;
        NumericUpDown? nuMinute;
        NumericUpDown? nuSecond;
        Button? BtnOK;
        Button? BtnCancel;
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            nuHour = e.NameScope.Get<NumericUpDown>("Hour");
            nuMinute = e.NameScope.Get<NumericUpDown>("Minute");
            nuSecond = e.NameScope.Get<NumericUpDown>("Second");
            BtnOK = e.NameScope.Get<Button>("BtnOK");
            BtnCancel = e.NameScope.Get<Button>("BtnCancel");

            if (BtnOK != null)
            {
                BtnOK.Click += OnAcceptButtonClicked;
            }
            if (BtnCancel != null)
            {
                BtnCancel.Click += OnDismissButtonClicked;
            }

        }

        private void OnDismissButtonClicked(object? sender, RoutedEventArgs e)
        {
            OnDismiss();
        }

        private void OnAcceptButtonClicked(object? sender, RoutedEventArgs e)
        {
            //Date = _syncDate;
            OnConfirmed();
        }

        public DateTime Value
        {
            get { return new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, (int)Hour, (int)Minute, (int)Second); }
            set
            {
                DateOnly = value.Date;
                Hour = value.Hour;
                Minute = value.Minute;
                Second = value.Second;
                //OnPropertyChanged();
            }
        }
    }
}
