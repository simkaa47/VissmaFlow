using CommunityToolkit.Mvvm.ComponentModel;
using EasyModbus;
using Microsoft.Extensions.Logging;
using System.IO.Ports;

namespace VissmaFlow.Core.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;
        private readonly Timer _timer;
        ModbusClient _modbusClient = new ModbusClient();

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger;
            _timer = new Timer(OnTimerExecute);
            _timer.Change(0, 2000);
        }

        [ObservableProperty]
        private int[] _registers = new int[10];

        [ObservableProperty]
        private string _portName;

        private string[] GetComPorts()
        {
            return SerialPort.GetPortNames();
        }

        public IEnumerable<string> Ports => GetComPorts();

        [ObservableProperty]
        private string _status;


        private void OnTimerExecute(object? state)
        {
            try
            {
                if(string.IsNullOrEmpty(PortName))
                {
                    throw new Exception("Необходимо задать имя порта");
                }
                if(!_modbusClient.Connected)
                {
                    Connect();
                    return;
                }
                Registers = _modbusClient.ReadHoldingRegisters(0, 10);
                Status = "Все получилось";
            }
            catch (Exception ex)
            {
                Status = $"{DateTime.Now}{ex.Message}({PortName} c настройками: Baudrate = {Baudrate}, Parity = {Parity}, StopBits = {StopBit})";
                Disconnect();
            }
        }


        [ObservableProperty]
        private Parity _parity = Parity.None;

        [ObservableProperty]
        private int _baudrate = 115200;

        [ObservableProperty]
        private StopBits _stopBit = System.IO.Ports.StopBits.One;

        public List<int> Baudrates { get; } = new List<int>
        {
            9600,19200,38500,57600,115200
        };

        public List<Parity> Parities { get; } = new List<Parity>
        {
            Parity.None, Parity.Even, Parity.Odd
        };

        public List<StopBits> StopBits { get; } = new List<StopBits>
        {
            System.IO.Ports.StopBits.One, System.IO.Ports.StopBits.Two
        };

        public void Disconnect()
        {
            try
            {
                _modbusClient.Disconnect();
            }
            catch (Exception ex)
            {

                Status = $"Отключение - {ex.Message}";
            }
        }

        public void Connect()
        {
            try
            {                
                _modbusClient.Baudrate = Baudrate;
                _modbusClient.Parity = Parity;
                _modbusClient.StopBits = StopBit;
                _modbusClient.SerialPort = PortName;
                 Status = $"{DateTime.Now.ToString("G")} выполняется подключение  c настройками: Baudrate = {Baudrate}, Parity = {Parity}, StopBits = {StopBit})";
                _modbusClient.Connect();
                if (_modbusClient.Connected)
                    Status = "Подключение выполнено";
                else
                    Status = "Подключение не выполнено";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
