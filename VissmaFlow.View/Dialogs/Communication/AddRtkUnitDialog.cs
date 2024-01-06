using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Models.Communication;

namespace VissmaFlow.View.Dialogs.Communication
{
    public class AddRtkUnitDialog : IRtkUnitDialog
    {
        public async Task<RtkUnit?> ShowDialog()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return null;
            }
            var unit = new RtkUnit();
            RtkDialogWindow window = new RtkDialogWindow(unit);
            await window.ShowDialogAsync();
            if (window.DialogResult) return unit;
            return null;
        }
    }
}
