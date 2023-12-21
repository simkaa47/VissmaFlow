using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.Models.Communication.Modbus;

namespace VissmaFlow.Core.Models.Parameters
{
    public partial class ParameterBase:EntityCommon
    {
        #region Имя
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Тип данных
        [ObservableProperty]
        private DataType _data;
        #endregion

        #region Описание
        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private string _description = string.Empty;
        #endregion

        #region Тип регистра
        [ObservableProperty]
        private ModbusRegType _modbusRegType;
        #endregion

        #region Номер Modbus регистра
        [Range(0, int.MaxValue)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private int _modbRegNum;
        #endregion

        #region Видимость параметра
        [ObservableProperty]
        private UserAccessLevel _userAccessLevel;
        #endregion

        #region Только для чтения
        [ObservableProperty]
        private bool _isReadOnly; 
        #endregion

        #region Номер бита в регистре(Для битовых переменных)
        [ObservableProperty]
        [Range(0, 15)]
        [NotifyDataErrorInfo]
        private int _bitNum;
        #endregion

        #region Минимальное значение        
        [ObservableProperty]        
        private string _minValueString = string.Empty;
        #endregion

        #region Максимальное значение        
        [ObservableProperty]        
        private string _maxValueString = string.Empty;
        #endregion

        #region Длина (если строка)
        [ObservableProperty]
        [Range(0, 255)]
        [NotifyDataErrorInfo]
        private int _strLength;
        #endregion

        #region Порядок байт
        [ObservableProperty]
        private ByteOrder _byteOrder;
        #endregion

        #region Нужность
        [ObservableProperty]
        private bool _isRequired;
        #endregion

        [NotMapped]
        public int ModbusUnitId { get; set; }

    }
}
