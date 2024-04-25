using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using VissmaFlow.Core.Infrastructure.Helpers;
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
            LoggingViewModel loggingViewModel,
            IndicationVm indicationVm,
            SingleMeasuresViewModel singleMeasuresViewModel,
            AccessViewModel accessViewModel,
            EventViewModel eventsViewModel,
            TrendSettigsViewModel trendSettigsViewModel)
        {
            _logger = logger;
            ParameterVm = parameterVm;
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
        public MainCommunicationService CommunicationService { get; }
        public CommunicationVm CommunicationVm { get; }
        public LoggingViewModel LoggingViewModel { get; }
        public IndicationVm IndicationVm { get; }
        public SingleMeasuresViewModel SingleMeasuresViewModel { get; }
        public AccessViewModel AccessViewModel { get; }
        public EventViewModel EventsViewModel { get; }
        public TrendSettigsViewModel TrendSettigsViewModel { get; }


        [ObservableProperty]
        private DateTime _setDateTime = DateTime.Now;


        [RelayCommand]
        private void SetTime()
        {
            if (SetDateTime > DateTime.MinValue)
            {
                if (OperatingSystem.IsLinux())
                {
                    string cmd = $"hwclock --set --date=\"{SetDateTime.ToString("yyyy-MM-dd HH:mm:ss")}\" --localtime";
                    ShellHelper.BashCommand(cmd);
                }
                else if(OperatingSystem.IsWindows())
                {
                    SYSTEMTIME st = new SYSTEMTIME();
                    st.wYear = (short)SetDateTime.Year; // must be short
                    st.wMonth = (short)SetDateTime.Month;
                    st.wDay = (short)SetDateTime.Day;
                    st.wHour = (short)SetDateTime.Hour;
                    st.wMinute = (short)SetDateTime.Minute;
                    st.wSecond = (short)SetDateTime.Second;
                    var res = SetSystemTime(ref st);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);




    }




}
