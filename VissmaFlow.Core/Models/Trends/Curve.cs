﻿using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        set 
        {             
                if(value is not null)SetProperty(ref _rtkUnit, value); 
        } 
    }
    #endregion

    #region Параметр 
    private ParameterBase? _parameter;
    public virtual ParameterBase? Parameter
    {
        get => _parameter;
        set { if (value is not null) SetProperty(ref _parameter, value); }
    }
    #endregion

    #region Видимость тренда
    //[ObservableProperty]
    private bool _isVisible;
    public bool IsVisible
    {
        get => _isVisible;
        set=> SetProperty(ref _isVisible, value);
    }
    #endregion

    [NotMapped]
    public ObservableCollection<DateTimePoint> Values { get; set; } = new ObservableCollection<DateTimePoint>();


}
