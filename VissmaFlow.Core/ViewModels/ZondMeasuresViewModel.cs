using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Text;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.Models.ZondMeasuring;

namespace VissmaFlow.Core.ViewModels
{
    public partial class ZondMeasuresViewModel : ObservableObject
    {
        private readonly ILogger<ZondMeasuresViewModel> _logger;
        private readonly CommunicationVm _communicationVm;
        private readonly IRepository<ZondMeasureSettings> _repository;
        private readonly ParameterVm _parameterVm;
        private readonly Timer _timer;
        private float _sum = 0;
        private int _cnt = 0;
        private ZondMeasure? _selectedZondMeasure;
        private ParameterBase? _source;

        public ZondMeasuresViewModel(ILogger<ZondMeasuresViewModel> logger, 
            CommunicationVm communicationVm,
            IRepository<ZondMeasureSettings> repository,
            ParameterVm parameterVm)
        {
            _logger = logger;
            _communicationVm = communicationVm;
            _repository = repository;
            _parameterVm = parameterVm;
            _timer = new Timer(OnTimer);
            InitAsync();
            Settings.PropertyChanged += async (s, args) =>
            {
                if (args.PropertyName == nameof(Settings.Source)
                || args.PropertyName == nameof(Settings.PipeType)
                || args.PropertyName == nameof(Settings.MeasTime))
                {
                    await ClearPoints();
                }

            };
        }

        private async void OnTimer(object? o)
        {
            if(_selectedZondMeasure is not null && _source is not null) 
            {  
                var parValue = _source.GetValue();
                if(parValue.isNumeric)
                {
                    _sum += parValue.value;
                    _cnt++;
                }
                _selectedZondMeasure.LeftTime--;
                if(_selectedZondMeasure.LeftTime<=0)
                {
                    SwitchTimerOff();
                    _selectedZondMeasure.IsMeasured = false;
                    _selectedZondMeasure.Date = DateTime.Now;
                    if (_cnt > 0) _selectedZondMeasure.Result = _sum / _cnt;
                    await SaveSettingsAsync();
                }
            }
        }

        private void SwitchTimerOn()
        {
            _timer.Change(1000, 1000);
            _sum = 0;
            _cnt = 0;
            if(_selectedZondMeasure is not null)
            {
                _selectedZondMeasure.IsMeasured = true;
                _selectedZondMeasure.LeftTime = Settings.MeasTime;
            }
        }

        private void SwitchTimerOff()
        {
            IsMeasured = false;
            _timer.Change(Timeout.Infinite,
                      Timeout.Infinite);
        }

        [ObservableProperty]
        private bool _isMeasured;


        [ObservableProperty]
        private ZondMeasureSettings _settings = new ZondMeasureSettings();

        [ObservableProperty]
        private float _averageResult;

        public IEnumerable<ParameterBase>? Parameters => GetParameters();

        private async void InitAsync()
        {
            try
            {
                var sett = new ZondMeasureSettings {Source = GetParameters()?.FirstOrDefault() };
                Settings = (await _repository.InitAsync(new List<ZondMeasureSettings> { sett }, 1)).FirstOrDefault() ?? sett;
                await RecalculateMeasPoints();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Инициализация данных о данных о настройках измерений зондом - {ex.Message}");
            }
        }

        private IEnumerable<ParameterBase>? GetParameters()
        {
            return _parameterVm.Parameters?.Where(p => p is not Parameter<bool> && p is not Parameter<string>);
        }

        #region Commands
        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            try
            {
                await _repository.UpdateAsync(Settings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение данных о данных о настройках измерений зондом - {ex.Message}");
            }
        }

        [RelayCommand]
        private void Measure(object o)
        {            
            if(o is ZondMeasure point && !IsMeasured && Settings.Source is not null)
            {
                _source = _communicationVm.RtkUnits?.FirstOrDefault()?.
                    Parameters.Where(p => p.Id == Settings.Source.Id).
                    FirstOrDefault();
                _selectedZondMeasure = point;
                IsMeasured = true;
                SwitchTimerOn();
            }
        }

        [RelayCommand]
        private void CalculateAverage()
        {
            AverageResult = Settings.ZondMeasures?.Average(z => z.Result) ?? 0;
        }

        #endregion

        private async Task ClearPoints()
        {
            AverageResult = 0;
            await RecalculateMeasPoints();
        }


        private async Task RecalculateMeasPoints()
        {
            if (Settings.Height == 0 || Settings.Source is null)
                Settings.ZondMeasures = new List<ZondMeasure> { };
            else if (Settings.PipeType == PipeType.Rectangle)
                await CalculatePointsForRectangle();
            else
                await CalculatePointsForRound();

        }

        private async Task GetNewPoints(List<ZondMeasure> points)
        {
            if (Settings.ZondMeasures is null || points.Count != Settings.ZondMeasures.Count)
                Settings.ZondMeasures = points;
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    points[i].Distance += 30;
                    Settings.ZondMeasures[i].Index = i + 1;
                    if (Settings.ZondMeasures[i].Distance != points[i].Distance)
                    {
                        Settings.ZondMeasures[i].Date = DateTime.MinValue;
                        Settings.ZondMeasures[i].Distance = points[i].Distance;
                        Settings.ZondMeasures[i].Result = 0.0f;
                    }
                }
            }
            await SaveSettingsAsync();
        }

        private async Task CalculatePointsForRectangle()
        {
            var points = new List<ZondMeasure>();
            if (Settings.Height > 200)
            {                
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.1f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.4f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.4f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.1f) });
            }
            else
            {
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.2f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.2f) });
            }
            await GetNewPoints(points);
        }

        private async Task CalculatePointsForRound()
        {
            var points = new List<ZondMeasure>();
            if (Settings.Height > 300)
            {                
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.054f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.28f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.28f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.054f) });
            }
            else
            {                
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height * 0.12f) });
                points.Add(new ZondMeasure { Distance = (int)(Settings.Height - Settings.Height * 0.12f) });
            }
            await GetNewPoints(points);
        }

    }
}
