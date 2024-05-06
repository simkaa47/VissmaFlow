using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Infrastructure.Helpers;
using VissmaFlow.Core.Models.Administration;

namespace VissmaFlow.Core.ViewModels
{
    public partial class PcSettingsViewModel : ObservableObject
    {
        private readonly ILogger<PcSettingsViewModel> _logger;
        private readonly IRepository<PcSettings> _repository;

        public PcSettingsViewModel(ILogger<PcSettingsViewModel> logger, IRepository<PcSettings> repository)
        {
            _logger = logger;
            _repository = repository;
            InitAsync();
        }


        [ObservableProperty]
        private PcSettings? _pcSettings;


        private async void InitAsync()
        {
            try
            {
                var setts = new List<PcSettings> { new PcSettings { Password = "linaro" } };
                PcSettings = (await _repository.InitAsync(setts, 1)).FirstOrDefault();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Инициализация настроек ПК - {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SaveSettings()
        {
            try
            {
                if (PcSettings is not null)
                    await _repository.UpdateAsync(PcSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение настроек ПК - {ex.Message}");
            }
        }

        [ObservableProperty]
        private DateTime _setDateTime = DateTime.Now;

        [RelayCommand]
        private void SetTime()
        {
            if (SetDateTime > DateTime.MinValue && PcSettings is not null)
            {
                if (OperatingSystem.IsLinux() && PcSettings.Password is not null)
                {
                    string cmd = $"echo {PcSettings.Password} | sudo -S date --set=\"{SetDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}\"";
                    ShellHelper.BashCommand(cmd);
                }
                else if (OperatingSystem.IsWindows())
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
