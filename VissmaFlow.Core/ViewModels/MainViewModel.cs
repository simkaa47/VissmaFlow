using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Services.Communication;

namespace VissmaFlow.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;


        public MainViewModel(ILogger<MainViewModel> logger,
            ParameterVm parameterVm,
            MainCommunicationService communicationService,
            CommunicationVm communicationVm,
            AccessViewModel accessViewModel,
            EventViewModel eventsViewModel, 
            TrendSettigsViewModel trendSettigsViewModel)
        {
            _logger = logger;
            ParameterVm = parameterVm;
            CommunicationService = communicationService;
            CommunicationVm = communicationVm;
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
        public MainCommunicationService CommunicationService { get; }
        public CommunicationVm CommunicationVm { get; }
        public AccessViewModel AccessViewModel { get; }
        public EventViewModel EventsViewModel { get; }
        public TrendSettigsViewModel TrendSettigsViewModel { get; }
    }
}
