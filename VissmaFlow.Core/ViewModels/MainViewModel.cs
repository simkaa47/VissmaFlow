using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Services.Communication;

namespace VissmaFlow.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;


        public MainViewModel(ILogger<MainViewModel> logger,
            ParameterVm parameterVm,
            PcSettingsViewModel pdcSettingsViewModel,
            MainCommunicationService communicationService,
            CommunicationVm communicationVm,
            LoggingViewModel loggingViewModel,
            IndicationVm indicationVm,
            SingleMeasuresViewModel singleMeasuresViewModel,
            AccessViewModel accessViewModel,
            EventViewModel eventsViewModel,
            TrendSettigsViewModel trendSettigsViewModel)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            PdcSettingsViewModel = pdcSettingsViewModel;
            CommunicationService = communicationService;
            CommunicationVm = communicationVm;
            LoggingViewModel = loggingViewModel;
            IndicationVm = indicationVm;
            SingleMeasuresViewModel = singleMeasuresViewModel;
            AccessViewModel = accessViewModel;
            EventsViewModel = eventsViewModel;
            TrendSettigsViewModel = trendSettigsViewModel;
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
        public PcSettingsViewModel PdcSettingsViewModel { get; }
        public MainCommunicationService CommunicationService { get; }
        public CommunicationVm CommunicationVm { get; }
        public LoggingViewModel LoggingViewModel { get; }
        public IndicationVm IndicationVm { get; }
        public SingleMeasuresViewModel SingleMeasuresViewModel { get; }
        public AccessViewModel AccessViewModel { get; }
        public EventViewModel EventsViewModel { get; }
        public TrendSettigsViewModel TrendSettigsViewModel { get; }

    }

}
