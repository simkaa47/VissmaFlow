using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.FileDialog;

namespace VissmaFlow.View.Dialogs.FileDialogs
{
    public class AvaloniaFileDialog : IFileDialog
    {
        public async Task<string> GetFile()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return string.Empty;
            }
            var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);
            if (topLevel == null) return string.Empty;
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = false
            });

            if (files is not null && files.Count > 0)
            {
                return files[0].Path.LocalPath;
            }
            return string.Empty;
        }

        public async Task<string> GetDirectory()
        {
            if (!(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                return string.Empty;
            }
            var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);   
            if (topLevel == null) return string.Empty;
            var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                AllowMultiple = false
            });
            
            if (files is not null && files.Count > 0)
            {
                return files[0].Path.LocalPath;
            }
            return string.Empty;
        }
    }
}
