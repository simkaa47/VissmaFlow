using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Contracts.FileDialog;
using VissmaFlow.Core.Models.Logging;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class LoggingViewModel : ObservableObject
    {
        private readonly ILogger<LoggingViewModel> _logger;
        private readonly IFileDialog _fileDialog;
        private readonly IRepository<LogSettings> _logRepository;
        [ObservableProperty]
        private LogSettings? _settings;

        public LoggingViewModel(ILogger<LoggingViewModel> logger, 
            IFileDialog fileDialog, IRepository<LogSettings> logRepository)
        {
            _logger = logger;
            _fileDialog = fileDialog;
            _logRepository = logRepository;
            InitAsync();
        }

        private async void InitAsync()
        {
            try
            {
                var setts = new List<LogSettings>
            {
                new LogSettings{MinPeriod = 1000}
            };
                Settings = (await _logRepository.InitAsync(setts, 1)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация конфигурации логирования - {ex.Message}");
            }
            
        }


        [RelayCommand]
        private async Task GetPath()
        {
            if (Settings is null) return;
            Settings.Path = await _fileDialog.GetPath();
        }


        [RelayCommand]
        private async Task SaveConfigAsync()
        {
            try
            {
                if (Settings is null) return;
                await _logRepository.UpdateAsync(Settings);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Сохранение конфигурации логирования - {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task AddParameterAsync()
        {
            if (Settings is null) return;
            if (Settings.Parameters is null) Settings.Parameters = new List<ParameterBase?>();
            Settings.Parameters.Add(null);
            await SaveConfigAsync();
            Settings.Parameters = new List<ParameterBase?>(Settings.Parameters);
        }


    }
}
