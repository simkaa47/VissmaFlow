using CommunityToolkit.Mvvm.ComponentModel;
using VissmaFlow.Core.Infrastructure.DataAccess;

namespace VissmaFlow.Core.Models.Administration
{
    public partial class PcSettings:EntityCommon
    {
        [ObservableProperty]
        private string? _password = string.Empty;
        [ObservableProperty]
        private Themes _theme;
    }
}
