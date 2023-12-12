using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Common;

namespace VissmaFlow.View.Dialogs.Common
{
    public class AskDialog : IQuestionDialog
    {
        public async Task<bool> Ask(string title, string message)
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return false;
            }
            AskDialogWindow window = new AskDialogWindow();
            window.Title = title;
            window.Content.Text = message;
            await window.ShowDialog(desktop.MainWindow);
            return window.DialogResult;

        }
    }
}
