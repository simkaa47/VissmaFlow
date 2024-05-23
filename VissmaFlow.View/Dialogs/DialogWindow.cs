using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using System.Threading;
using System.Threading.Tasks;

namespace VissmaFlow.View.Dialogs
{
    public partial class DialogWindow : UserControl
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected bool needToCloseDialog;


        public bool DialogResult { get; set; }
        protected void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            needToCloseDialog = true;
        }

        private Control? TmpContent;

        public async Task ShowDialogAsync()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)) return;
            var border = desktop.MainWindow!.Get<Border>("MainContentBorder");
            if (border is null) return;
            TmpContent = border.Child;
            border.Child = this;
            await Task.Run(() =>
            {
                while (!needToCloseDialog){ Thread.Sleep(100); }
            });
            needToCloseDialog = false;
            border.Child = TmpContent;
        }


        protected void Cancel_Click(object sender, RoutedEventArgs e)
        {
            needToCloseDialog = true;
        }
    }
}
