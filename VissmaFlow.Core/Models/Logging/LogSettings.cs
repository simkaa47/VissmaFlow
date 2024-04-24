using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.Attributes;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.Logging
{
    public partial class LogSettings : EntityCommon
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [IsDirectoryExist]
        private string? _path;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(1000,100000)]
        private int _minPeriod;

        private List<LogCell>? _cells;

        public virtual List<LogCell>? Cells
        {
            get => _cells;
            set=> SetProperty(ref _cells, value);
        }




    }
}
