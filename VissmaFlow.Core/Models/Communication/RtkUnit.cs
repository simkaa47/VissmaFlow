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

        #region Параметры
        private IEnumerable<ParameterBase> _parameters = new List<ParameterBase>();

        [NotMapped]
        public IEnumerable<ParameterBase> Parameters 
        { 
            get => _parameters; 
            set => SetProperty(ref _parameters, value);
            
        }
        #endregion        

        
        
        private bool _connected = true;
        [NotMapped]
        public bool Connected
        {
            get => _connected;
            set => SetProperty(ref _connected, value);
        }

    }
}
