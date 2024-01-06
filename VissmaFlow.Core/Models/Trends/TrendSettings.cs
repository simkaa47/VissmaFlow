using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VissmaFlow.Core.Infrastructure.DataAccess;

namespace VissmaFlow.Core.Models.Trends;

public partial class TrendSettings:EntityCommon
{
    #region Макс кол-во секунд для тренда
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(1, 10000)]
    private int _maxTimeSeconds;
    #endregion

    #region Частота сканирования, мс
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(200, 100000)]
    private int _scanFrequence; 
    #endregion
}
