using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Contracts.FileDialog;
using VissmaFlow.Core.Models.Logging;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class LoggingViewModel : ObservableObject
    {
        private Timer _timer;
        private Timer _secondsTimer;

        private bool _isWriting;

        private object _locker = new object();


        private readonly ILogger<LoggingViewModel> _logger;
        private readonly IFileDialog _fileDialog;
        private readonly IRepository<LogSettings> _logRepository;
        [ObservableProperty]
        private LogSettings? _settings;

        public CommunicationVm CommunicationVm { get; }
        public ParameterVm ParameterVm { get; }

        public LoggingViewModel(ILogger<LoggingViewModel> logger,
            IFileDialog fileDialog, IRepository<LogSettings> logRepository,
            CommunicationVm communicationVm, ParameterVm parameterVm)
        {
            _logger = logger;
            _fileDialog = fileDialog;
            _timer = new Timer(OnTimer);
            _timer.Change(Timeout.Infinite,
                       Timeout.Infinite);
            _secondsTimer = new Timer(OnSecondsTimer);
            _secondsTimer.Change(Timeout.Infinite,
                       Timeout.Infinite);
            _logRepository = logRepository;
            CommunicationVm = communicationVm;
            ParameterVm = parameterVm;
            InitAsync();
        }


        [ObservableProperty]
        private bool _isLogging;
        [ObservableProperty]
        private string? _errStatus;
        [ObservableProperty]
        private string _fileName = string.Empty;

        [ObservableProperty]
        private TimeSpan _currentLogTime = TimeSpan.Zero;

        private int _timeHours = 0;
        [Range(0,23)]
        public int TimeHours
        {
            get=> _timeHours;
            set
            {
                if(value>=0 && value <=23)
                    SetProperty(ref _timeHours, value); 
            }
        }

        private int _timeMinutes = 1;
        [Range(1, 59)]
        public int TimeMinutes
        {
            get => _timeMinutes;
            set
            {
                if (value >= 1 && value <= 59)
                    SetProperty(ref _timeMinutes, value);
            }
        }

        private void OnSecondsTimer(object? o)
        {
            lock (_locker)
            {
                CurrentLogTime += TimeSpan.FromSeconds(1);
                if(CurrentLogTime.TotalSeconds > (TimeHours*3600 + TimeMinutes*60))
                {
                    SwitchTimerOff();
                }
            }
        }


        private void OnTimer(object? o)
        {
            lock (_locker)
            {
                if (Settings?.Cells is null) return;
                StringBuilder builder = new StringBuilder();
                foreach (var cell in Settings.Cells)
                {
                    if (cell.RtkUnit is not null && cell.Parameter is not null)
                    {
                        var par = cell.RtkUnit.Parameters.Where(p => p.Id == cell.Parameter.Id).FirstOrDefault();
                        var value = GetValueFromParameter(par);
                        if (value is not null)
                        {
                            builder.Append($"{value.ToString()}\t");

                        }
                    }
                }
                if (builder.Length > 0)
                {
                    builder.Insert(0, $"{DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss.f")}\t");
                    WriteString(builder.ToString());
                }
            }                 
        }

        private async void InitAsync()
        {
            try
            {
                var setts = new List<LogSettings>
            {
                new LogSettings{MinPeriod = 1}
            };
                Settings = (await _logRepository.InitAsync(setts, 1)).FirstOrDefault();
                if(Settings is not null && Settings.Cells is not null)
                {
                    var index = 0;
                    foreach (var cell in Settings.Cells)
                    {
                        if(cell is not null)
                        {
                            index++;
                            cell.Index = index;
                        }
                    }
                }
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
            Settings.Path = await _fileDialog.GetDirectory();
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
        private async Task DeleteCellAsync(object o)
        {
            if (Settings is null) return;
            if (o is not LogCell cell) return;
            if (Settings.Cells is null) return;
            Settings.Cells.Remove(cell);
            await SaveConfigAsync();
            Settings.Cells = new List<LogCell>(Settings.Cells);
        }


        [RelayCommand]
        private async Task AddParameterAsync()
        {
            if (Settings is null) return;
            if (Settings.Cells is null) Settings.Cells = new List<LogCell>();
            Settings.Cells.Add(new LogCell());
            await SaveConfigAsync();
            Settings.Cells = new List<LogCell>(Settings.Cells);
        }

        [RelayCommand]
        private void SwitchTimer()
        {
            if (IsLogging)
                SwitchTimerOff();
            else
                SwitchTimerOn();
        }


        private void SwitchTimerOn()
        {
            if (Settings is not null && Settings.Cells is not null)
            {                
                var dt = DateTime.Now;
                FileName = $"log_{dt.ToString("ddMMyyyy")}__{dt.ToString("HHmmss")}";
                StringBuilder builder = new StringBuilder();
                var dtHeader = $"Дата/время\t";
                foreach (var cell in Settings.Cells)
                {
                    if (cell.RtkUnit is not null && cell.Parameter is not null)
                    {
                        builder.Append($"{cell.RtkUnit.Name} : {cell.Parameter.Description}");
                        builder.Append("\t");
                    }
                }
                if (builder.Length > 0)
                {
                    if (Settings.MinPeriod < 1)
                        Settings.MinPeriod = 1;
                    _timer.Change(100,
                           Settings.MinPeriod*1000);
                    _secondsTimer.Change(1000,1000);
                    CurrentLogTime = new TimeSpan(0, 0, 0);
                    IsLogging = true;
                    builder.Insert(0, dtHeader);
                    WriteString(builder.ToString());
                }
                else
                    ErrStatus = "Нет подходящих данных для логирования";
            }
        }

        private void SwitchTimerOff()
        {
            _timer.Change(Timeout.Infinite,
                       Timeout.Infinite);
            _secondsTimer.Change(Timeout.Infinite,
                       Timeout.Infinite);
            IsLogging = false;
        }

        private void WriteString(string str)
        {
            try
            {
                if (!Directory.Exists(Settings!.Path))
                    throw new Exception("Проверьте директорию сохранения файла!");
                if (!_isWriting)
                {
                    using (StreamWriter writer = new StreamWriter($"{Settings.Path}/{FileName}.txt", true))
                    {
                        writer.WriteLine(str); ;
                    }
                    _isWriting = false;
                    ErrStatus = null;
                }
            }
            catch (Exception ex)
            {
                ErrStatus = ex.Message;
                _isWriting = false;
                SwitchTimerOff();
            }
        }


        private object? GetValueFromParameter(ParameterBase? par)
        {
            if (par is null) return null;
            if (par is ParameterShort parameterShort)
                return parameterShort.Value;
            else if (par is ParameterUshort parameterUshort)
                return parameterUshort.Value;
            else if (par is ParameterInt parameterInt)
                return parameterInt.Value;
            else if (par is ParameterUint parameterUint)
                return parameterUint.Value;
            else if (par is ParameterFloat parameterFloat)
                return parameterFloat.Value;
            else if (par is ParameterDouble parameterDouble)
                return parameterDouble.Value;
            else if (par is ParameterString parameterString)
                return parameterString.Value;
            else if (par is ParameterBool parameterBool)
                return parameterBool.Value;
            return 0;
        }


    }
}
