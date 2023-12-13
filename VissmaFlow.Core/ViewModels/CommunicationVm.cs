using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Communication;

namespace VissmaFlow.Core.ViewModels
{
    public partial class CommunicationVm : ObservableObject
    {
        private readonly ILogger<CommunicationVm> _logger;
        private readonly IRepository<CommSettings> _commSettRepository;

        public CommunicationVm(ILogger<CommunicationVm> logger, IRepository<CommSettings> commSettRepository)
        {
            _logger = logger;
            _commSettRepository = commSettRepository;
            InitAsync();
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

        [ObservableProperty]
        private CommSettings? _commSettings;

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
            System.IO.Ports.StopBits.One, System.IO.Ports.StopBits.Two
        };

        private async void InitAsync()
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

        private string[] GetComPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
