using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
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

        private bool _isWriting;


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


        private void OnTimer(object? o)
        {
            if (Settings?.Cells is null) return;
            StringBuilder builder = new StringBuilder();
            foreach (var cell in Settings.Cells)
            {
                if(cell.RtkUnit is not null && cell.Parameter is not null)
                {
                    var par = cell.RtkUnit.Parameters.Where(p => p.Id == cell.Parameter.Id).FirstOrDefault();
                    var value = GetValueFromParameter(par);
                    if(value is not null)
                    {
                        builder.Append($"{value.ToString()}\t");
                        
                    }
                }
            }
            if(builder.Length>0)
            {
                builder.Insert(0, $"{DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss.f")}\t");
                WriteString(builder.ToString());
            }            
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
                    if (Settings.MinPeriod < 1000)
                        Settings.MinPeriod = 1000;
                    _timer.Change(100,
                           Settings.MinPeriod);
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
