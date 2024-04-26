using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Globalization;
using VissmaFlow.View.UserControls.Keyboard;
using VissmaFlow.View.ViewModels;

namespace VissmaFlow.View
{
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU", true);
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";

            CultureInfo.CurrentUICulture = new CultureInfo("ru-RU", true);
            CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator = ",";
            WindowState = WindowState.FullScreen;           
            this.AddHandler<GotFocusEventArgs>(Control.GotFocusEvent, openVirtualKeyboard);
           

        }

        StyledElement keyboard;

        private void OnKeyboardInitialized(object? sender, RoutedEventArgs e)
        {            
            if(sender is not null && sender is StyledElement control)
            {
                keyboard = control;
                control.DataContext = new KeyBoardViewModel();
            }
        }

        


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void openVirtualKeyboard(object? sender, GotFocusEventArgs e)
        {
            if (e.Source!.GetType() == typeof(TextBox) && keyboard is not  null && keyboard.DataContext is KeyBoardViewModel vm)
            {

                if (!vm.IsOskVisible) 
                {                    
                    WeakReferenceMessenger.Default.Send(new PassObjectMsg(e.Source));
                    WeakReferenceMessenger.Default.Send(new OskControlMsg(true));
                    e.Handled = true;                   
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
    }
}