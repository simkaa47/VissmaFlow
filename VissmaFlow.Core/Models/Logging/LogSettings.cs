using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.Logging
{
    public partial class LogSettings:EntityCommon
    {
        [ObservableProperty]
        private string? _path;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(1000,100000)]
        private int _minPeriod;

        private List<ParameterBase>? _parameters;
        public virtual List<ParameterBase>? Parameters
        {
            get => _parameters;
            set => SetProperty(ref _parameters, value);
        }

    }
}
