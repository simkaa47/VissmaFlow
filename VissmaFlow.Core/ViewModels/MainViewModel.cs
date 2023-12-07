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
            _timer.Change(0, 5000);
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
                Status = $"{ex.Message}({PortName})";
                Disconnect();
            }
        }


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
                _modbusClient.Baudrate = 115200;
                _modbusClient.Parity = Parity.None;
                _modbusClient.StopBits = StopBits.One;
                _modbusClient.SerialPort = PortName;
                _modbusClient.Connect();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
