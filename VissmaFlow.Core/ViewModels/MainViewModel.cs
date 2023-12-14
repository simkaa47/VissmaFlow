using CommunityToolkit.Mvvm.ComponentModel;
using EasyModbus;
using Microsoft.Extensions.Logging;
using System.IO.Ports;

namespace VissmaFlow.Core.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;        
        ModbusClient _modbusClient = new ModbusClient();

        public MainViewModel(ILogger<MainViewModel> logger, 
            ParameterVm parameterVm, 
            CommunicationVm communicationVm)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            CommunicationVm = communicationVm;            
        }
        
        public ParameterVm ParameterVm { get; }
        public CommunicationVm CommunicationVm { get; }        

        
    }
}
