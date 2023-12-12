using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using System.ComponentModel;
using System.Timers;
using VissmaFlow.Core.Infrastructure.Attributes;

namespace VissmaFlow.Core.Models.Parameters
{
    public partial class Parameter<T> : ParameterBase, INotifyDataErrorInfo where T : IComparable
    {

        public Parameter()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimerElapsed;
            PropertyChanged += OnChanging;
            
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

        #region IsOnlyRead
        [ObservableProperty]
        private bool _isOnlyRead;
        #endregion

        #region ValidationOk
        [ObservableProperty]
        private bool _validationOk;
        #endregion

        #region IsWriting
        [ObservableProperty]
        private bool _isWriting;
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

        [ObservableProperty]
        private int _hz;

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

        private void OnChanging(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MinValueString))
                MinValue = (T?)GetValueFromStr(MinValueString, false);
            else if (e.PropertyName == nameof(MaxValueString))
                MaxValue = (T?)GetValueFromStr(MaxValueString, true);
            else if (e.PropertyName == nameof(MaxValue) && MaxValue != null)
                MaxValueString = MaxValue.ToString();
            else if (e.PropertyName == nameof(MinValue) && MinValue != null)
                MinValueString = MinValue.ToString();
        }


        private object? GetValueFromStr(string input, bool maxValue)
        {
            switch (Data)
            {   
                case DataType.boolean:
                    if(bool.TryParse(input, out bool parsedBool))
                    {
                        return parsedBool;
                    }
                    return maxValue ? true : false;
                case DataType.int16:
                    if (short.TryParse(input, out short parsedShort))
                    {
                        return parsedShort;
                    }
                    return maxValue ? short.MaxValue : short.MinValue;
                case DataType.uint16:
                    if (ushort.TryParse(input, out ushort parsedUShort))
                    {
                        return parsedUShort;
                    }
                    return maxValue ? ushort.MaxValue : ushort.MinValue;
                case DataType.int32:
                    if (int.TryParse(input, out int parsedInt))
                    {
                        return parsedInt;
                    }
                    return maxValue ? int.MaxValue : int.MinValue;
                case DataType.uint32:
                    if (uint.TryParse(input, out uint parsedUInt))
                    {
                        return parsedUInt;
                    }
                    return maxValue ? uint.MaxValue : uint.MinValue;
                case DataType.float32:
                    if (float.TryParse(input, out float parsedFloat))
                    {
                        return parsedFloat;
                    }
                    return maxValue ? float.MaxValue : float.MinValue;
                case DataType.double64:
                    if (double.TryParse(input, out double parsedDouble))
                    {
                        return parsedDouble;
                    }
                    return maxValue ? double.MaxValue : double.MinValue;
                case DataType.str:
                    return input;
                default:
                    return null;
            }

        }


        
    }
}
