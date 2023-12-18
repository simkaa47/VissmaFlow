using CommunityToolkit.Mvvm.ComponentModel;
using EasyModbus;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using VissmaFlow.Core.Services.Communication;

namespace VissmaFlow.Core.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;        
        ModbusClient _modbusClient = new ModbusClient();

        public MainViewModel(ILogger<MainViewModel> logger, 
            ParameterVm parameterVm, MainCommunicationService communicationService,
            CommunicationVm communicationVm)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            CommunicationService = communicationService;
            CommunicationVm = communicationVm;            
        }
        
        public ParameterVm ParameterVm { get; }
        public MainCommunicationService CommunicationService { get; }
        public CommunicationVm CommunicationVm { get; }        

        
    }
}
