using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class CommunicationVm : ObservableObject
    {
        private readonly ILogger<CommunicationVm> _logger;
        private readonly IRepository<CommSettings> _commSettRepository;
        private readonly IRepository<RtkUnit> _rtkUnitRepository;
        private readonly IRtkUnitDialog _rtkUnitDialog;
        private readonly IQuestionDialog _questionDialog;

        public CommunicationVm(ILogger<CommunicationVm> logger,
            IRepository<CommSettings> commSettRepository, 
            IRepository<RtkUnit> _rtkUnitRepository, 
            IRtkUnitDialog rtkUnitDialog, IQuestionDialog questionDialog)
        {
            _logger = logger;
            _commSettRepository = commSettRepository;
            this._rtkUnitRepository = _rtkUnitRepository;
            _rtkUnitDialog = rtkUnitDialog;
            _questionDialog = questionDialog;
            InitCommSettingsAsync();
            InitRtkUnitsAsync();
        }

        [RelayCommand]
        private async Task SaveCommSettAsync()
        {
            if (CommSettings is null) return;
            try
            {
                _logger.LogInformation($"Сохранение настроек связи с РТК");
                await _commSettRepository.UpdateAsync(CommSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение настроек связи с РТК - {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task AddRtkUnitAsync()
        {
            var rtk = await _rtkUnitDialog.ShowDialog();
            if(rtk is not null && !rtk.HasErrors)
            {
                _logger.LogInformation($"Добавление РТК \"{rtk.Name}\"");
                try
                {
                    await _rtkUnitRepository.AddAsync(rtk);
                    InitRtkUnitsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Добавление РТК \"{rtk.Name}\" - {ex.Message}");
                }
            }
        }

        [RelayCommand]
        private async Task DeleteRtkAsync(object o)
        {
            if (!(o is RtkUnit rtk)) return;
            if(await _questionDialog.Ask("Удаление РТК", $"Удалить {rtk.Name}?"))
            {
                try
                {
                    _logger.LogInformation($"Выполняется удаление РТК {rtk.Name}");
                    await _rtkUnitRepository.DeleteAsync(rtk);
                    InitRtkUnitsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Удаление РТК {rtk.Name} - {ex.Message}");
                }
            }

        }

        [RelayCommand]
        private async Task SaveAllAsync()
        {
            if (RtkUnits is null) return;            
            foreach (var rtk in RtkUnits)
            {
                try
                {
                    await _rtkUnitRepository.UpdateAsync(rtk);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Сохранение РТК {rtk.Name} - {ex.Message}");
                }
            }
        }

        [ObservableProperty]
        private CommSettings? _commSettings;

        [ObservableProperty]
        private IEnumerable<RtkUnit>? _rtkUnits;

        public IEnumerable<string> Ports => GetComPorts();
        public List<int> Baudrates { get; } = new List<int>
        {
            9600,19200,38500,57600,115200
        };
        public List<Parity> Parities { get; } = new List<Parity>
        {
            Parity.None, Parity.Even, Parity.Odd
        };
        public List<StopBits> StopBitsList { get; } = new List<StopBits>
        {
            StopBits.One, StopBits.Two
        };

        private async void InitCommSettingsAsync()
        {
            try
            {
                var setts = new List<CommSettings>
                {
                    new CommSettings
                    {
                        Baudrate = 115200, Parity = Parity.None, StopBitsNum = StopBits.One, PortName = "/dev/ttyS5"
                    }
                };
                CommSettings = (await _commSettRepository.InitAsync(setts, 1)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация настроек соедирнения - {ex.Message}");
            }
        }

        private async void InitRtkUnitsAsync()
        {
            try
            {
                var rtks = new List<RtkUnit>
                {
                    new RtkUnit {Name="RTK1", UnitId = 1}
                };
                RtkUnits = await _rtkUnitRepository.InitAsync(rtks, 1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация РТК - {ex.Message}");
            }
        }

        private string[] GetComPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
