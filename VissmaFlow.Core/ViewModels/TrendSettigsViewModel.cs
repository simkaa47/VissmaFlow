using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Trends;
using VissmaFlow.Core.Services.Trends;

namespace VissmaFlow.Core.ViewModels
{
    public partial class TrendSettigsViewModel:ObservableObject
    {
        private readonly ILogger<TrendSettigsViewModel> _logger;
        private readonly IRepository<Curve> _curveRepository;
        private readonly IRepository<TrendSettings> _settingsRepository;

        public TrendSettings? TrendSettings { get; set; }

        public TrendSettigsViewModel(ILogger<TrendSettigsViewModel> logger, 
            ParameterVm parameterVm,
            IRepository<Curve> curveRepository, 
            IRepository<TrendSettings> settingsRepository)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            _curveRepository = curveRepository;
            _settingsRepository = settingsRepository;
            InitAsync();
        }
        [ObservableProperty]
        private List<Curve>? _curves;
        public ParameterVm ParameterVm { get; }

        private async void InitAsync()
        {
            try
            {
                var initSetts = TrendSettingsFactory.GetCurves();
                Curves = (await _curveRepository.InitAsync(initSetts, initSetts.Count)).ToList();
                TrendSettings = (await _settingsRepository.InitAsync(TrendSettingsFactory.GetTrendSettings(), 1)).FirstOrDefault();
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Инициализация настроек кривых графика - {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            try
            {
                _logger.LogInformation($"Сохранение настроек nhtylf");
                if (Curves is null) return;
                await _curveRepository.UpdateAllAsync(Curves);
                if(TrendSettings is not null)
                {
                    await _settingsRepository.UpdateAsync(TrendSettings);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение настроек кривых -  {ex.Message}");
            }
        }

        



    }
}
