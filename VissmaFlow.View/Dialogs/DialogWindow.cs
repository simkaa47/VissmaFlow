using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using System.ComponentModel;
using VissmaFlow.View.UserControls.Keyboard;

namespace VissmaFlow.View.Dialogs
{
    public partial class DialogWindow : Window, ITextInputMethodRoot
    {       

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.HasSystemDecorations = false;
            if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                this.WindowState = WindowState.FullScreen;
                this.Position = desktop.MainWindow.Position;
                this.Width = desktop.MainWindow.Width;
                this.Height = desktop.MainWindow.Height;
            }
        }


        public bool DialogResult { get; set; }
        protected void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        protected void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private VirtualKeyboardTextInputMethod virtualKeyboardTextInput = new VirtualKeyboardTextInputMethod();
        ITextInputMethodImpl ITextInputMethodRoot.InputMethod
        {
            get
            {
                return virtualKeyboardTextInput;
            }
        }

    }
}
