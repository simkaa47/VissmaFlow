using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.FileDialog;

namespace VissmaFlow.Core.ViewModels
{
    public partial class LoggingViewModel : ObservableObject
    {
        private readonly ILogger<LoggingViewModel> _logger;
        private readonly IFileDialog _fileDialog;

        public LoggingViewModel(ILogger<LoggingViewModel> logger, IFileDialog fileDialog)
        {
            _logger = logger;
            _fileDialog = fileDialog;
        }


        [RelayCommand]
        private async Task GetPath()
        {
            var ehd = await _fileDialog.GetPath();
        }

    }
}
