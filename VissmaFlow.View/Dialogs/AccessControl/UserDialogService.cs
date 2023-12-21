using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using VissmaFlow.Core.Infrastructure.AccessControl;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.Dialogs.AccessControl
{
    internal class UserDialogService : IAccessDialogService
    {
        public async Task<bool> ShowDialog(User user)
        {
            
            if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                UserDialogWindow userWindow = new UserDialogWindow(user);
                await userWindow.ShowDialog(desktop.MainWindow);
                return userWindow.DialogResult;
            }
            return false;
        }
    }
}
