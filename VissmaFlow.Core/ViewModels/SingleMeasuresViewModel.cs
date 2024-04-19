using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.SingleMeasures;
using VissmaFlow.Core.Services.SingleMeasures;

namespace VissmaFlow.Core.ViewModels
{
    public partial class SingleMeasuresViewModel : ObservableObject
    {
        private readonly IRepository<SingleMeasureSettings> _singleMeasRepository;
        private readonly ILogger<SingleMeasuresViewModel> _logger;
        [ObservableProperty]
        private SingleMeasureSettings? _settings;

        public SingleMeasuresViewModel(IRepository<SingleMeasureSettings> singleMeasRepository, 
            ILogger<SingleMeasuresViewModel> logger)
        {
            _singleMeasRepository = singleMeasRepository;
            _logger = logger;
            InitAsync();
        }

        private async void InitAsync()
        {
            try
            {
                Settings = (await _singleMeasRepository.InitAsync(SingleMeasuresFactory.Seed(), 1)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация параметров единичных измерений - {ex.Message}");
            }            
        }


        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            try
            {
                if (Settings is null) return;
                await _singleMeasRepository.UpdateAsync(Settings);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение параметров единичных измерений - {ex.Message}");
            }
        }


    }
}
