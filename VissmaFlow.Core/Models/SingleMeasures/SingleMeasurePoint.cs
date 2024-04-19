﻿using CommunityToolkit.Mvvm.ComponentModel;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Models.SingleMeasures
{
    public partial class SingleMeasurePoint : EntityCommon
    {
        
        private ParameterBase? _avgValue;
        public virtual ParameterBase? AvgValue
        {
            get => _avgValue;
            set=> SetProperty(ref _avgValue, value);
        }

        private ParameterBase? _destination;
        public virtual ParameterBase? Destination
        {
            get => _destination;
            set => SetProperty(ref _destination, value);
        }       

    }
}
