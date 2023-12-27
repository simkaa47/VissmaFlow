using Avalonia.Controls;
using Avalonia.Input.TextInput;
using Avalonia.Markup.Xaml;
using VissmaFlow.View.UserControls.Keyboard;

namespace VissmaFlow.View
{
    public partial class MainWindow : Window, ITextInputMethodRoot
    {
        public MainWindow()
        {           
            InitializeComponent();
            WindowState = WindowState.FullScreen;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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