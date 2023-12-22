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


        public MainViewModel(ILogger<MainViewModel> logger,
            ParameterVm parameterVm, MainCommunicationService communicationService,
            CommunicationVm communicationVm, AccessViewModel accessViewModel)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            CommunicationService = communicationService;
            CommunicationVm = communicationVm;
            AccessViewModel = accessViewModel;
            _timer = new Timer(UpdateTime);
            _timer.Change(0, 1000);
        }

        #region Timer
        Timer _timer;
        private void UpdateTime(object? obj)
        {
            PcTime = DateTime.Now;
        }

        [ObservableProperty]
        private DateTime _pcTime; 
        #endregion

        public ParameterVm ParameterVm { get; }
        public MainCommunicationService CommunicationService { get; }
        public CommunicationVm CommunicationVm { get; }
        public AccessViewModel AccessViewModel { get; }
    }
}
