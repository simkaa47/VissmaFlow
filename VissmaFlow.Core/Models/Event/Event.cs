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
        [NotMapped]
        [ObservableProperty]
        private bool _isActive;
        #endregion

        #region Условие срабатывания
        [ObservableProperty]
        private EventCondition _eventCondition;
        #endregion

        #region Сравниваемое значение
        [ObservableProperty]
        private float _compareValue = 0; 
        #endregion

    }
}
