using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace VissmaFlow.View.Dialogs
{
    public partial class DialogWindow : Window
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SystemDecorations = SystemDecorations.None;
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
    }
}
