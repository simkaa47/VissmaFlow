using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.FileDialog;

namespace VissmaFlow.View.Dialogs.FileDialogs
{
    public class AvaloniaFileDialog : IFileDialog
    {
        public async Task<string> GetPath()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return string.Empty;
            }
            var provider = desktop.MainWindow?.StorageProvider;
            if (provider is null) return string.Empty;
            var files = provider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                AllowMultiple = false
            }).Result;
            if (files is not null && files.Count > 0)
            {
                return files[0].Path.LocalPath;
            }
            return string.Empty;
        }
    }
}
