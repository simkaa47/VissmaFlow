using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.SingleMeasures
{
    public partial class SingleMeasureSettings : EntityCommon
    {
        [ObservableProperty]
        private int _duration;

        private int _curTime;
        [NotMapped]
        public int CurTime
        {
            get => _curTime;
            set=> SetProperty(ref _curTime, value);
        }


        private ParameterBase? _source;
        public virtual ParameterBase? Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }


        private List<SingleMeasurePoint>? _points;
        public virtual List<SingleMeasurePoint>? Points
        {
            get => _points;
            set=> SetProperty(ref _points, value);
        }


    }
}
