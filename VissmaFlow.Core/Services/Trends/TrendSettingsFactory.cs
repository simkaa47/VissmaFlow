﻿using VissmaFlow.Core.Models.Trends;

namespace VissmaFlow.Core.Services.Trends
{
    public class TrendSettingsFactory
    {
        public static List<Curve> GetCurves()
        {
            return new List<Curve>
            {
                new Curve{Color="#FFFF0000",IsVisible = true },
                new Curve{Color="#FF00FFFF",IsVisible = true },
                new Curve{Color="#FF000000",IsVisible = true },
                new Curve{Color="#FF00FF00",IsVisible = true },
                new Curve{Color="#FFFFFF00",IsVisible = true },
                new Curve{Color="#FFFFA500",IsVisible = true },
                new Curve{Color="#FFFF0000",IsVisible = false },
                new Curve{Color="#FFFF0000",IsVisible = false }
            };
        }

        public static List<TrendSettings> GetTrendSettings() 
        {
            return new List<TrendSettings> { 
                new TrendSettings
                {
                    MaxTimeSeconds = 1000,
                    ScanFrequence = 1000
                } 
            };
        }
    }
}
