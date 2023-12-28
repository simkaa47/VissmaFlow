using CommunityToolkit.Mvvm.ComponentModel;

using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.Trends;

public partial class Curve:EntityCommon
{
    #region Цвет
    [ObservableProperty]
    private string _color = "#0000FFFF";
    #endregion

    #region РТК
    private RtkUnit? _rtkUnit;
    public virtual RtkUnit? RtkUnit
    {
        get => _rtkUnit;
        set => SetProperty(ref _rtkUnit, value);
    }
    #endregion

    #region Параметр 
    private ParameterBase? _parameter;
    public virtual ParameterBase? Parameter
    {
        get => _parameter;
        set => SetProperty(ref _parameter, value);
    }
    #endregion

    #region Видимость тренда
    [ObservableProperty]
    private bool _isVisible; 
    #endregion


}
