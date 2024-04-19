using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.View.UserControls.Keyboard.Layout;
using VissmaFlow.View.ViewModels;

namespace VissmaFlow.View.UserControls.Keyboard;

public enum VirtualKeyboardState
{
    Default,
    Shift,
    Capslock,
    AltCtrl
}
public partial class VirtualKeyboard : UserControl
{
    private static List<Type> Layouts { get; } = new List<Type>();
    private static Func<Type>? DefaultLayout { get; set; }

    public static void AddLayout<TLayout>() where TLayout : KeyboardLayout => Layouts.Add(typeof(TLayout));

    public static void SetDefaultLayout(Func<Type> getDefaultLayout) => DefaultLayout = getDefaultLayout;

    public TextBox TextBox_ { get; }
    public Button AcceptButton_ { get; }
    public string targetLayout { get; set; }

    private TextBox? sourceObject;
    public TransitioningContentControl TransitioningContentControl_ { get; }

    public IObservable<VirtualKeyboardState> KeyboardStateStream => _keyboardStateStream;
    private readonly BehaviorSubject<VirtualKeyboardState> _keyboardStateStream;



    public VirtualKeyboard()
    {
        InitializeComponent();
        TextBox_ = this.Get<TextBox>("TextBox");
        TransitioningContentControl_ = this.Get<Avalonia.Controls.TransitioningContentControl>("TransitioningContentControl");
        AcceptButton_ = this.Get<Button>("AcceptButton");

        AcceptButton_.AddHandler(Button.ClickEvent, acceptClicked);
        WeakReferenceMessenger.Default.Register<PassObjectMsg>(this, async (r, m) =>
        {
            if (m.Value is TextBox textBox)
            {
                if (textBox.Tag is DataType.float32 || textBox.Tag is DataType.double64)
                    TransitioningContentControl_.Content = new FloatKeyboard();
                else if (textBox.Tag is DataType.int16 || textBox.Tag is DataType.uint16
                    || textBox.Tag is DataType.uint32 || textBox.Tag is DataType.int32)
                    TransitioningContentControl_.Content = new NumericKeyboard();
                else TransitioningContentControl_.Content = Activator.CreateInstance(DefaultLayout!.Invoke());


                TextBox_.Text = textBox.Text;
                sourceObject = textBox;
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                Dispatcher.UIThread.Post(() =>
                {
                    TextBox_.Focus();
                    if (!string.IsNullOrEmpty(TextBox_.Text))
                        TextBox_.CaretIndex = TextBox_.Text.Length;
                });
            }
        });

        Initialized += async (sender, args) =>
        {

            if (targetLayout == null)
            {
                TransitioningContentControl_.Content = Activator.CreateInstance(DefaultLayout!.Invoke());
            }
            else
            {
                var layout = Layouts.FirstOrDefault(x => x.Name.ToLower().Contains(targetLayout.ToLower()));
                if (layout != null)
                {
                    TransitioningContentControl_.Content = Activator.CreateInstance(layout);
                }
                else
                {
                    TransitioningContentControl_.Content = Activator.CreateInstance(DefaultLayout!.Invoke());
                }
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
            Dispatcher.UIThread.Post(() =>
            {
                TextBox_.Focus();
                if (!string.IsNullOrEmpty(TextBox_.Text))
                    TextBox_.CaretIndex = TextBox_.Text.Length;
            });
        };

        KeyDown += (sender, args) =>
        {
            TextBox_.Focus();
            if (args.Key == Key.Escape)
            {
                TextBox_.Text = "";
            }
            else if (args.Key == Key.Enter)
            {
                sourceObject!.Text = TextBox_.Text;
                ExecCmd(sourceObject);
                WeakReferenceMessenger.Default.Send(new OskControlMsg(false));
            }
        };
        _keyboardStateStream = new BehaviorSubject<VirtualKeyboardState>(VirtualKeyboardState.Default);
    }

    private void acceptClicked(object? sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new OskControlMsg(false));
    }


    private void SetCaretIndex(TextBox textBox, bool decrement)
    {
        if (TextBox_.Text is null) return;
        if (!decrement && textBox.CaretIndex < textBox.Text!.Length)
        {
            textBox.CaretIndex++;
        }
        else if (decrement && textBox.CaretIndex > 0)
        {
            textBox.CaretIndex--;
        }
        TextBox_.Focus();
    }

    public void ProcessText(string text)
    {
        TextBox_.Focus();
        if (TextBox_.Text is null)
            TextBox_.Text = text;
        else
            TextBox_.Text = TextBox_.Text!.Insert(TextBox_.CaretIndex, text);
        SetCaretIndex(TextBox_, false);
        if (_keyboardStateStream.Value == VirtualKeyboardState.Shift)
        {
            _keyboardStateStream.OnNext(VirtualKeyboardState.Default);
        }
    }

    public void Accept()
    {
        WeakReferenceMessenger.Default.Send(new OskControlMsg(false));
    }

    public void ProcessKey(Key key)
    {
        if (key == Key.LeftShift || key == Key.RightShift)
        {
            if (_keyboardStateStream.Value == VirtualKeyboardState.Shift)
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Default);
            }
            else
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Shift);
            }
        }
        else if (key == Key.Right) SetCaretIndex(TextBox_, false);
        else if (key == Key.Left) SetCaretIndex(TextBox_, true);
        else if (key == Key.RightAlt)
        {
            if (_keyboardStateStream.Value == VirtualKeyboardState.AltCtrl)
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Default);
            }
            else
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.AltCtrl);
            }
        }
        else if (key == Key.CapsLock)
        {
            if (_keyboardStateStream.Value == VirtualKeyboardState.Capslock)
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Default);
            }
            else
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Capslock);
            }
        }
        else
        {
            if (key == Key.Clear)
            {
                TextBox_.Text = "";
                TextBox_.Focus();
            }
            else if (key == Key.Enter || key == Key.ImeAccept)
            {
                sourceObject!.Text = TextBox_.Text;
                ExecCmd(sourceObject);
                WeakReferenceMessenger.Default.Send(new OskControlMsg(false));
            }
            else if (key == Key.Help)
            {
                _keyboardStateStream.OnNext(VirtualKeyboardState.Default);
                if (TransitioningContentControl_.Content is KeyboardLayout layout)
                {
                    var index = Layouts.IndexOf(layout.GetType());
                    if (Layouts.Count - 1 > index)
                    {
                        TransitioningContentControl_.Content = Activator.CreateInstance(Layouts[index + 1]);
                    }
                    else
                    {
                        TransitioningContentControl_.Content = Activator.CreateInstance(Layouts[0]);
                    }
                }
            }
            else if (key == Key.Back)
            {

                if (TextBox_.Text != null && TextBox_.Text.Length > 0 && TextBox_.CaretIndex>0)
                {
                    TextBox_.Text = TextBox_.Text.Remove(TextBox_.CaretIndex-1, 1);
                    TextBox_.CaretIndex--;
                }

            }
            else
            {
                TextBox_.Focus();

                //InputManager.Instance.ProcessInput(new RawKeyEventArgs(KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, (Window)TextBox.GetVisualRoot(), RawKeyEventType.KeyDown, key, RawInputModifiers.None));
                //InputManager.Instance.ProcessInput(new RawKeyEventArgs(KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, (Window)TextBox.GetVisualRoot(), RawKeyEventType.KeyUp, key, RawInputModifiers.None));
            }
        }
    }

    private void ExecCmd(TextBox? tb)
    {
        if (tb is null) return;
        var binding = tb.KeyBindings.FirstOrDefault();
        if (binding is not null)
        {
            var cmd = binding.Command;
            if (cmd is not null)
            {
                cmd.Execute(binding.CommandParameter);
            }
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
