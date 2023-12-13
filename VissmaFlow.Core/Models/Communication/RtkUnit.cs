using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.DataAccess;

namespace VissmaFlow.Core.Models.Communication
{
    public partial class RtkUnit:EntityCommon
    {
        #region Имя
        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;
        #endregion

        #region Номер в  сети
        [Range(0, 255)]
        [ObservableProperty]
        [NotifyDataErrorInfo]
        private int _unitId = 1; 
        #endregion
    }
}
