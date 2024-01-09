using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.Indication
{
    public partial class IndicationCell:EntityCommon
    {
        #region РТК
        private RtkUnit? _rtkUnit;
        public virtual RtkUnit? RtkUnit
        {
            get => _rtkUnit;
            set
            {
                if (value is not null)
                    SetProperty(ref _rtkUnit, value);
            }
        }
        #endregion

        #region Параметр
        private ParameterBase? _parameter;
        public virtual ParameterBase? Parameter
        {
            get => _parameter;
            set
            {
                if (value is not null)
                    SetProperty(ref _parameter, value);
            }
        } 
        #endregion

        [ObservableProperty]
        private string? _measUnit;

        [ObservableProperty]
        private bool _isRequired;

        
        [NotMapped]
        public ParameterBase? RealParameter
        {
            get => RtkUnit?.Parameters.Where(p=>p.Id == Parameter?.Id).FirstOrDefault();            
        }

        
    }
}
