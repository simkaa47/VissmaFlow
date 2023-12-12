using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Contracts.Parameters;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class ParameterVm : ObservableObject
    {
        private readonly ILogger<ParameterVm> _logger;
        private readonly IQuestionDialog _questionDialog;
        private readonly VissmaDbContext _dbContext;
        private readonly IParameterDialogService _parameterDialogService;

        public ParameterVm(ILogger<ParameterVm> logger,
            IQuestionDialog questionDialog,
            VissmaDbContext dbContext, 
            IParameterDialogService parameterDialogService)
        {
            _logger = logger;
            _questionDialog = questionDialog;
            _dbContext = dbContext;
            _parameterDialogService = parameterDialogService;
            InitParameters();
        }
        [ObservableProperty]
        private List<ParameterBase>? _parameters;

        #region Добавить параметр
        [RelayCommand]
        private async Task AddParameter()
        {
            var par = await _parameterDialogService.ShowDialog();
            if (par is not null && !par.HasErrors)
            {
                _logger.LogInformation($"Добавление параметра \"{par.Description}\"");
                try
                {
                    await _dbContext.Set<ParameterBase>().AddAsync(par);
                    await _dbContext.SaveChangesAsync();
                    InitParameters();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Добавление параметра {par.Description} - {ex.Message}");
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
                   _dbContext.Set<ParameterBase>().Remove(par);
                    await _dbContext.SaveChangesAsync();
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
                    _dbContext.Entry(par).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
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
                var parBases = await _dbContext.Set<ParameterBase>()
                    .AsNoTracking()
                    .ToListAsync();
                Parameters = parBases.Select(p => CreateParameter(p)).ToList();
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
                    par = new Parameter<bool>() {MinValue = false, MaxValue = true };
                    break;
                case DataType.int16:
                    par = new Parameter<short>() { MinValue = short.MinValue, MaxValue = short.MaxValue };
                    break;
                case DataType.uint16:
                    par = new Parameter<ushort>() { MinValue = ushort.MinValue, MaxValue = ushort.MaxValue };
                    break;
                case DataType.int32:
                    par = new Parameter<int>(){ MinValue = int.MinValue, MaxValue = int.MaxValue };
                    break;
                case DataType.uint32:
                    par = new Parameter<uint>() { MinValue = uint.MinValue, MaxValue = uint.MaxValue };
                    break;
                case DataType.float32:
                    par = new Parameter<float>() { MinValue = float.MinValue, MaxValue = float.MaxValue };
                    break;
                case DataType.double64:
                    par = new Parameter<double>() { MinValue = double.MinValue, MaxValue = double.MaxValue };
                    break;
                case DataType.str:
                    par = new Parameter<string>();
                    break;
                default:
                    break;
            }
            pBase.Adapt(par);
            return par;
        }
    }
}
