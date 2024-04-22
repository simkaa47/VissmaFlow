using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.Models.SingleMeasures;
using VissmaFlow.Core.Services.Communication;
using VissmaFlow.Core.Services.SingleMeasures;

namespace VissmaFlow.Core.ViewModels
{
    public partial class SingleMeasuresViewModel : ObservableObject
    {
        private readonly IRepository<SingleMeasureSettings> _singleMeasRepository;
        private readonly ILogger<SingleMeasuresViewModel> _logger;
        [ObservableProperty]
        private SingleMeasureSettings? _settings;
        [ObservableProperty]
        private IEnumerable<SingleMeasuresInstance>? _devices;

        private SingleMeasuresInstance? _selectedDevice;
        public SingleMeasuresInstance? SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (value is not null)
                    SetProperty(ref _selectedDevice, value);
            }
        }



        public SingleMeasuresViewModel(IRepository<SingleMeasureSettings> singleMeasRepository,
            ILogger<SingleMeasuresViewModel> logger,
            ParameterVm parameterVm,
            MainCommunicationService comminicationService,
            CommunicationVm communicationVm)
        {
            _singleMeasRepository = singleMeasRepository;
            _logger = logger;
            ParameterVm = parameterVm;
            ComminicationService = comminicationService;
            CommunicationVm = communicationVm;
            InitAsync();
        }

        public ParameterVm ParameterVm { get; }
        public MainCommunicationService ComminicationService { get; }
        public CommunicationVm CommunicationVm { get; }

        private async void InitAsync()
        {
            try
            {
                Settings = (await _singleMeasRepository.InitAsync(SingleMeasuresFactory.Seed(), 1)).FirstOrDefault();
                GetDevices();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация параметров единичных измерений - {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task AddSingleMeasPointAsync()
        {
            if (Settings is null) return;
            if (Settings.Points is null) Settings.Points = new List<SingleMeasurePoint>();
            Settings.Points.Add(new SingleMeasurePoint { });
            await SaveSettingsAsync();
            Settings.Points = new List<SingleMeasurePoint>(Settings.Points);
            GetDevices();
        }


        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            try
            {
                if (Settings is null) return;
                await _singleMeasRepository.UpdateAsync(Settings);
                GetDevices();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Сохранение параметров единичных измерений - {ex.Message}");
            }
        }

        [RelayCommand]
        private void SingleMeasure(object par)
        {
            if (par is null || par is not SingleMeasurePoint point) return;
            if (SelectedDevice is not null && SelectedDevice.MeasureSettings?.Source is not null)
            {
                if (!point.SingleMeasFlag)
                    point.MeasureCompletedEvent += WriteParameter;
                point.SingleMeasExecute(SelectedDevice.MeasureSettings.Duration, SelectedDevice.MeasureSettings?.Source);
                
            }

        }

        private void WriteParameter(SingleMeasurePoint point,ParameterBase par)
        {
            point.MeasureCompletedEvent -= WriteParameter;
            ComminicationService.WriteParameter(par);
        }


        private void GetDevices()
        {
            if (CommunicationVm.RtkUnits is null) return;
            if (Settings is null) return;
            int id = 1;
            var devices = new List<SingleMeasuresInstance>();
            foreach (var rtk in CommunicationVm.RtkUnits)
            {
                if (rtk is not null)
                {
                    devices.Add(new SingleMeasuresInstance
                    {
                        RtkUnit = rtk,
                        MeasureSettings = new SingleMeasureSettings
                        {

                            Source = rtk.Parameters.Where(p => p.Id == Settings?.Source?.Id).FirstOrDefault(),
                            Duration = Settings.Duration,
                            Points = Settings.Points?.Select(p => new SingleMeasurePoint
                            {
                                Id = id++,
                                AvgValue = rtk.Parameters.Where(par => p.AvgValue?.Id == par.Id).FirstOrDefault(),
                                Destination = rtk.Parameters.Where(par => p.Destination?.Id == par.Id).FirstOrDefault()
                            }).ToList()
                        }
                    });
                }
            }
            Devices = devices;
        }


    }
}
