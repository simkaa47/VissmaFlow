using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.Core.Models.SingleMeasures
{
    public partial class SingleMeasurePoint : EntityCommon
    {
        Timer? _timer;

        public event Action<SingleMeasurePoint, ParameterBase>? MeasureCompletedEvent;

        private ParameterBase? _avgValue;
        public virtual ParameterBase? AvgValue
        {
            get => _avgValue;
            set
            {
                if (value is not null)
                    SetProperty(ref _avgValue, value);
            }
        }

        private ParameterBase? _destination;
        public virtual ParameterBase? Destination
        {
            get => _destination;
            set
            {
                if (value is not null)
                    SetProperty(ref _destination, value);
            }
        }

        private int _curTime;
        [NotMapped]
        public int CurTime
        {
            get => _curTime;
            set => SetProperty(ref _curTime, value);
        }

        private bool _singleMeasFlag;
        [NotMapped]
        public bool SingleMeasFlag
        {
            get => _singleMeasFlag;
            set => SetProperty(ref _singleMeasFlag, value);
        }

        private float _sum = 0;
        private int _cnt = 0;
        private ParameterBase? _source;

        public void SingleMeasExecute(int time, ParameterBase? source)
        {
            if (SingleMeasFlag) return;
            if (source is null) return;
            if (source is ParameterString || source is ParameterBool) return;
            _timer = new Timer(OnTimer);
            CurTime = 30;
            _cnt = 0;
            _sum = 0;
            _source = source;
            SingleMeasFlag = true;
            _timer.Change(1000, 1000);
        }

        private void OnTimer(object? o)
        {
            if (_source is null) return;
            var parValue = _source.GetValue();
            if(parValue.isNumeric)
            {
                _cnt++;
                _sum += parValue.value;
            }   
            CurTime--;
            if(CurTime <= 0)
            {
                _timer?.Change(Timeout.Infinite,
                       Timeout.Infinite);
                if (_cnt>0)
                {
                    var result = _sum / _cnt;
                    SingleMeasFlag = false;

                    if (AvgValue is null) return;
                    if (AvgValue is ParameterShort avgValueShort)
                        avgValueShort.WriteValue = (short)result;
                    else if (AvgValue is ParameterDouble avgValueDouble)
                        avgValueDouble.WriteValue = result;
                    else if (AvgValue is ParameterUshort avgValueUshort)
                        avgValueUshort.WriteValue = (ushort)result;
                    else if (AvgValue is ParameterUint avgValueUint)
                        avgValueUint.WriteValue = (uint)result;
                    else if (AvgValue is ParameterInt avgValueInt)
                        avgValueInt.WriteValue = (int)result;
                    else if (AvgValue is ParameterFloat avgValueFloat)
                        avgValueFloat.WriteValue = result;
                    MeasureCompletedEvent?.Invoke(this, AvgValue);                    
                }                 

            }

        }

    }
}
