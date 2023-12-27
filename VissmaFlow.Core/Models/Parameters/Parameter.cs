using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using System.ComponentModel;
using System.Timers;
using VissmaFlow.Core.Contracts.Parameters;
using VissmaFlow.Core.Infrastructure.Attributes;

namespace VissmaFlow.Core.Models.Parameters
{
    public partial class Parameter<T> : ParameterBase, INotifyDataErrorInfo where T : IComparable
    {

        public Parameter()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimerElapsed;            
        }

        #region MinValue
        [ObservableProperty]
        private T? _minValue;
        #endregion

        #region MaxValue
        [ObservableProperty]
        private T? _maxValue;
        #endregion

        #region Таймер
        System.Timers.Timer timer;

        void RestartTimer()
        {
            if (timer.Enabled) timer.Stop();
            timer.Start();
        }

        void OnTimerElapsed(object? source, ElapsedEventArgs e)
        {
            timer.Stop();
            WriteValue = Value;
        }


        #endregion

        

        #region Value
        private T? _value;
        public T? Value
        {
            get => _value;
            set
            {
                if (SetProperty(ref _value, value))
                    WriteValue = value;
                IsWriting = false;
            }
        }
        #endregion        

        #region WriteValue
        T? _writeValue;

        [InDiapasone(nameof(MinValue), nameof(MaxValue))]
        public T? WriteValue
        {
            get => _writeValue;
            set
            {
                if (value != null)
                {
                    ValidateProperty(value, nameof(WriteValue));
                    if (value.CompareTo(Value) != 0)
                    {
                        RestartTimer();
                    }
                    SetProperty(ref _writeValue, value);                    
                }
            }
        }
        #endregion     
        
    }
}
