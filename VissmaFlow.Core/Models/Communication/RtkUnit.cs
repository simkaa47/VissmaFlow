using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

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

        [NotMapped]
        public IEnumerable<ParameterBase> Parameters { get; set; } = new List<ParameterBase>();
    }
}
