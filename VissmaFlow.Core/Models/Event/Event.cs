using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.Event
{
    public partial class Event : EntityCommon
    {
        #region Тип события
        [ObservableProperty]
        private EventType _eventType;
        #endregion

        #region РТК
        private RtkUnit? _rtkUnit;
        public virtual RtkUnit? RtkUnit
        {
            get => _rtkUnit;
            set 
            {
                if(value != null)
                SetProperty(ref _rtkUnit, value);
            } 
        }
        #endregion

        #region Параметр
        private ParameterBase? _parameter;
        public virtual ParameterBase? Parameter
        {
            get=> _parameter;            
            set
            {
                if (value != null)
                    SetProperty(ref _parameter, value);
            }
        }
        #endregion

        #region Активное сообщение
        [ObservableProperty]
        private string? _activeMessage;
        #endregion

        #region Неактивное сообщение
        [ObservableProperty]
        private string? _nonActiveMessage;
        #endregion

        #region Активность        
        private bool _isActive;
        [NotMapped]
        public bool IsActive
        {
            get => _isActive;
            set=>SetProperty(ref _isActive, value);
        }
        #endregion

        #region Условие срабатывания
        [ObservableProperty]
        private EventCondition _eventCondition;
        #endregion

        #region Сравниваемое значение
        [ObservableProperty]
        private float _compareValue = 0;
        #endregion


        private bool _unVisisble;
        [NotMapped]
        public bool UnVisisble
        {
            get => _unVisisble;
            set { 
                if (value) SetProperty(ref _unVisisble, value); 
            }
        }

        
        private DateTime _lastActiveTime;
        [NotMapped]
        public DateTime LastActiveTime
        {
            get=> _lastActiveTime;
            set => SetProperty(ref _lastActiveTime, value);
        }

    }
}
