using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Parameters;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.View.Dialogs.Parameters
{
    public class ParameterDialogService : IParameterDialogService
    {
        public async Task<ParameterBase?> ShowDialog()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return null;
            }
            var par = new ParameterBase();
            ParameterChangeWindow window  = new ParameterChangeWindow(par);
            await window.ShowDialog(desktop.MainWindow);
            if (window.DialogResult) return par;
            return null;
        }
    }
}
