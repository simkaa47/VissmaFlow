using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.ZondMeasuring
{
    public partial class ZondMeasureSettings:EntityCommon
    {
        [ObservableProperty]
        private PipeType _pipeType = PipeType.Round;

        [ObservableProperty]
        [Range(0.1,100000)]
        private float _height = 400;

        
        private ParameterBase? _source;  
        public virtual ParameterBase? Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }


        [ObservableProperty]
        private int _measTime = 30;

        private List<ZondMeasure>? _zondMeasures;
        public virtual List<ZondMeasure>? ZondMeasures
        {
            get => _zondMeasures;
            set => SetProperty(ref _zondMeasures, value);
        }
    }
}
