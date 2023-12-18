using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.TextInput;

namespace VissmaFlow.View.UserControls.Keyboard
{
    public class VirtualKeyboardTextInputMethod : ITextInputMethodImpl
    {
        private bool _isOpen;
        private TextInputOptionsQueryEventArgs? _textInputOptions;
        public async void SetActive(bool active)
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return;
            }
            if (active && !_isOpen && _textInputOptions != null)
            {
                _isOpen = true;
                await VirtualKeyboard.ShowDialog(_textInputOptions);
                _isOpen = false;
                _textInputOptions = null;
                desktop.MainWindow.Focus(); // remove focus from the last control (TextBox)
            }
        }

        public void SetCursorRect(Rect rect) { }

        public void SetOptions(TextInputOptionsQueryEventArgs? options)
        {
            _textInputOptions = options;
        }

        public void Reset() { }
    }
}
