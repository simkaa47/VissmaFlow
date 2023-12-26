using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.Core.Services.Communication
{
    public partial class MainCommunicationService:ObservableObject
    {
        private readonly IComminicationService _askService;
        private readonly ILogger<MainCommunicationService> _logger;


        [ObservableProperty]
        private bool _connected;

        private Queue<ParameterBase> WriteCommands { get; } = new Queue<ParameterBase>();
        public ParameterVm ParameterVm { get; }

        public MainCommunicationService(IComminicationService askService, 
            ILogger<MainCommunicationService> logger, ParameterVm parameterVm)
        {
            _askService = askService;
            _logger = logger;
            ParameterVm = parameterVm;
            ReadProcessAsync();
        }

        


        private async void ReadProcessAsync()
        {
            await Task.Run(() => 
            {
                while (_askService != null) 
                {
                    try
                    {
                        Connected = _askService.Connected;
                        if (!Connected)
                            Thread.Sleep(2000);
                        while (WriteCommands.Count > 0)
                        {
                            var par = WriteCommands.Dequeue();
                            _askService.WriteParameter(par);
                        }
                        if (ParameterVm.CommunicationVm.RtkUnits is null) continue;
                        foreach (var rtk in ParameterVm.CommunicationVm.RtkUnits)
                        {
                            _askService.ReadAllData(rtk.Parameters, rtk.UnitId);
                        }                        
                        
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            });
        }

        [RelayCommand]
        private void WriteParameter(object o)
        {
            if (o is null) return;
            if (o is Parameter<ushort> parUshort)
                SetIsWritingFlag(parUshort);
            else if (o is Parameter<short> parShort)
                SetIsWritingFlag(parShort);
            else if (o is Parameter<bool> parBool)
                SetIsWritingFlag(parBool);
            else if (o is Parameter<double> parDouble)
                SetIsWritingFlag(parDouble);
            else if (o is Parameter<int> parInt)
                SetIsWritingFlag(parInt);
            else if (o is Parameter<uint> parUint)
                SetIsWritingFlag(parUint);
            else if (o is Parameter<float> parFloat)
                SetIsWritingFlag(parFloat);
            else if (o is Parameter<string> parstring)
                SetIsWritingFlag(parstring);
        }

        private void SetIsWritingFlag<T>(Parameter<T> parameter) where T : IComparable
        {
            if (parameter.ValidationOk && Connected)
            {
                parameter.IsWriting = true;
                WriteCommands.Enqueue(parameter);
            }
        }

    }
}
