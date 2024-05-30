using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;

namespace VissmaFlow.Core.Models.ZondMeasuring
{
    public partial class ZondMeasure : EntityCommon
    { 
        [ObservableProperty]
        private int _distance;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private float _result;

        private int _leftTime;
        [NotMapped]
        public int LeftTime
        {
            get => _leftTime;
            set=> SetProperty(ref _leftTime, value);    
        }

        private int _index;
        [NotMapped]
        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }


        private bool _isMeasured;
        [NotMapped]
        public bool IsMeasured
        {
            get=> _isMeasured;
            set=> SetProperty(ref _isMeasured, value);
        }
        
    }
}
