using CommunityToolkit.Mvvm.ComponentModel;
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
        public RtkUnit? RtkUnit
        {
            get => _rtkUnit;
            set=> SetProperty(ref _rtkUnit, value);
        }
        #endregion

        #region Параметр
        private ParameterBase? _parameter;
        public ParameterBase? Parameter
        {
            get=> _parameter;
            set=> SetProperty(ref _parameter, value);   
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
