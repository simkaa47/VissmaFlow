using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Communication.Modbus;

namespace VissmaFlow.Core.Models.Parameters
{
    public partial class ParameterBase:EntityCommon
    {
        #region Имя
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Описание
        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private string _description = string.Empty;
        #endregion

        #region Тип регистра
        [ObservableProperty]
        private MosbusRegType _modbusRegType;
        #endregion

        #region Номер Modbus регистра
        [Range(0, int.MaxValue)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private int _modbRegNum;
        #endregion

        #region Номер бита в регистре(Для битовых переменных)
        [ObservableProperty]
        [Range(0, 15)]
        [NotifyDataErrorInfo]
        private int _bitNum;
        #endregion

        #region Минимальное значение
        [Required]
        [ObservableProperty]
        [NotifyDataErrorInfo]
        private string _minValueString = string.Empty;
        #endregion

        #region Максимальное значение
        [Required]
        [ObservableProperty]
        [NotifyDataErrorInfo]
        private string _maxValueString = string.Empty;
        #endregion

        #region Длина (если строка)
        [ObservableProperty]
        [Range(0, 255)]
        [NotifyDataErrorInfo]
        private int _strLength; 
        #endregion

    }
}
