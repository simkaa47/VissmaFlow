using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Indication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class IndicationVm : ObservableObject
    {
        private readonly ILogger<IndicationVm> _logger;
        private readonly IRepository<IndicationCell> _repository;

        [ObservableProperty]
        private IEnumerable<IndicationCell>? _indicationCells;

        public IndicationVm(ILogger<IndicationVm> logger, 
            IRepository<IndicationCell> repository, 
            CommunicationVm communicationVm,
            ParameterVm parameterVm)
        {
            _logger = logger;
            _repository = repository;
            CommunicationVm = communicationVm;
            ParameterVm = parameterVm;
            InitAsync();
        }

        public CommunicationVm CommunicationVm { get; }
        public ParameterVm ParameterVm { get; }

        private async void InitAsync()
        {
            try
            {
                _logger.LogInformation($"Инициализация индикации");
                IndicationCells = await _repository.InitAsync(Enumerable.Range(0, 8)
                    .Select(i => new IndicationCell()), 8);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Инициализация индикации - {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task SaveAllAsync()
        {
            if (IndicationCells is null) return;
            try
            {
                _logger.LogInformation($"Сохранение индикации");
                await _repository.UpdateAllAsync(IndicationCells.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение индикации - {ex.Message}");
            }
        }

        


    }
}
