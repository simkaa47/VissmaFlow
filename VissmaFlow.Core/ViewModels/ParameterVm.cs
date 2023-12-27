using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Contracts.Parameters;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.Services.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class ParameterVm : ObservableObject
    {
        private readonly ILogger<ParameterVm> _logger;
        private readonly IQuestionDialog _questionDialog;
        private readonly IRepository<ParameterBase> _parameterRepository;        
        private readonly IParameterDialogService _parameterDialogService;

        public ParameterVm(ILogger<ParameterVm> logger, 
            CommunicationVm communicationVm,
            IQuestionDialog questionDialog,
            IRepository<ParameterBase> parameterRepository,
            IParameterDialogService parameterDialogService)
        {
            _logger = logger;
            CommunicationVm = communicationVm;
            _questionDialog = questionDialog;
            _parameterRepository = parameterRepository;            
            _parameterDialogService = parameterDialogService;
            InitParameters();
        }
        [ObservableProperty]
        private IEnumerable<ParameterBase>? _parameters;

        public CommunicationVm CommunicationVm { get; }



        #region Добавить параметр
        [RelayCommand]
        private async Task AddParameter()
        {
            var basePar = await _parameterDialogService.ShowDialog();
            if (basePar is not null && !basePar.HasErrors)
            {
                _logger.LogInformation($"Добавление параметра \"{basePar.Description}\"");
                try
                {
                    var par = CreateParameter(basePar);
                    await _parameterRepository.AddAsync(par);
                    InitParameters();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Добавление параметра {basePar.Description} - {ex.Message}");
                }                
            }            
        }
        #endregion

        #region Удалить параметр
        [RelayCommand]
        private async Task DeleteParameterAsync(object o)
        {
            if (!(o is ParameterBase par)) return;
            _logger.LogInformation($"Удаление параметра \"{par.Description}\"");
            try
            {
                if (await _questionDialog.Ask($"Удаление параметра {par.Description}", "Удалить ошибку?"))
                {
                    _logger.LogInformation($"Выполняется удаление параметра {par.Description}");
                    await _parameterRepository.DeleteAsync(par);
                    InitParameters();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Удаление параметра {par.Description} - {ex.Message}");
            }

        }
        #endregion

        #region Сохранить все параметры
        [RelayCommand]
        private async Task SaveParametersAsync()
        {
            if (Parameters is null) return;
            foreach (var par in Parameters)
            {
                if (par == null) continue;
                try
                {
                    await _parameterRepository.UpdateAsync(par);
                    InitParameters();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Сохранение параметра {par.Description} - {ex.Message}");

                }
            }
        }
        #endregion


        private async void InitParameters()
        {
            try
            {
                Parameters = await _parameterRepository.InitAsync(ParametersDataFactory.GetDefaultParameters(),1);                
                if (CommunicationVm.RtkUnits is null) return;
                foreach (var rtk in CommunicationVm.RtkUnits)
                {
                    rtk.Parameters = Parameters.Select(p =>
                    {
                        var par = CreateParameter(p);
                        par.Owner = rtk;
                        return par;
                    }).ToList();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Инициализация параметров - {ex.Message}");
            }
        }


        private  ParameterBase CreateParameter(ParameterBase pBase)
        {
            ParameterBase par = new ParameterBase();
            switch (pBase.Data)
            {
                case DataType.boolean:
                    par = new ParameterBool() { Data = pBase.Data, MinValue = false, MaxValue = true };
                    break;
                case DataType.int16:
                    par = new ParameterShort() { Data = pBase.Data, MinValue = short.MinValue, MaxValue = short.MaxValue };
                    break;
                case DataType.uint16:
                    par = new ParameterUshort() { Data = pBase.Data, MinValue = ushort.MinValue, MaxValue = ushort.MaxValue };
                    break;
                case DataType.int32:
                    par = new ParameterInt{ Data = pBase.Data, MinValue = int.MinValue, MaxValue = int.MaxValue };
                    break;
                case DataType.uint32:
                    par = new ParameterUint() { Data = pBase.Data, MinValue = uint.MinValue, MaxValue = uint.MaxValue };
                    break;
                case DataType.float32:
                    par = new ParameterFloat() { Data = pBase.Data, MinValue = float.MinValue, MaxValue = float.MaxValue };
                    break;
                case DataType.double64:
                    par = new ParameterDouble() {Data = pBase.Data, MinValue = double.MinValue, MaxValue = double.MaxValue };
                    break;
                case DataType.str:
                    par = new ParameterString() {Data = pBase.Data, MinValue = string.Empty};
                    break;
                default:
                    break;
            }
            pBase.Adapt(par);
            return par;
        }
    }
}
