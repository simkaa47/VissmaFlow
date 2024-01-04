using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.View.ViewModels
{
    public class TrendsViewModel : ObservableObject
    {
        private readonly ILogger<TrendsViewModel> _logger;
        private readonly TrendSettigsViewModel _trendSettigsViewModel;
        Timer? _timer;

        public TrendsViewModel(ILogger<TrendsViewModel> logger, TrendSettigsViewModel trendSettigsViewModel)
        {
            _logger = logger;
            _trendSettigsViewModel = trendSettigsViewModel;           
            InitAsync();
        }

        private  void InitAsync()
        {            
            if (_trendSettigsViewModel.Curves is null) return;
            Series = _trendSettigsViewModel
                .Curves
                .Where(c=>c.Parameter is not null && c.RtkUnit is not null)
                .Select(c => new LineSeries<DateTimePoint> 
                { 
                    Name = $"{c.RtkUnit?.Name} : {c.Parameter?.Description}",
                    Values = c.Values,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    IsVisible = c.IsVisible,
                    Fill = null,
                    Stroke = new SolidColorPaint(GetSKColor(c.Color)){ StrokeThickness = 2 }

                }).ToArray();
            _timer = new Timer(OnTimer);
            _timer.Change(0, 1000);
            
        }

        public ISeries[]? Series { get; set; }

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {                
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            }
        };

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
            };
        }

        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds;

            return secsAgo < 1
                ? "now"
                : $"{secsAgo:N0}s ago";
        }




        private SKColor GetSKColor(string colorString)
        {
            var color = Color.Parse(colorString);
            var skColor = new SKColor(color.R, color.G, color.B, color.A);
            return skColor;
        }


        private void OnTimer(object? o)
        {
            if (_trendSettigsViewModel.Curves is null) return;
            foreach (var c in _trendSettigsViewModel.Curves)
            {
                if(c.RtkUnit is not null && c.Parameter is not null)
                {
                    var par = c.RtkUnit.Parameters.Where(p=>p.Id == c.Parameter.Id).FirstOrDefault();   
                    if(par is not null)
                    {
                        Dispatcher.UIThread.InvokeAsync(new Action(() =>
                        {
                            c.Values.Add(new DateTimePoint(DateTime.Now, GetValueFromParameter(par)));

                        }));
                    }
                }
            }
        }     
        
        private double GetValueFromParameter(ParameterBase par)
        {
            if (par is ParameterFloat parameterFloat)
                return parameterFloat.Value;
            if (par is ParameterString parameterString)
                return 0;
            else if (par is ParameterDouble parameterDouble)
                return parameterDouble.Value;
            else if (par is  ParameterShort parameterShort)
                return parameterShort.Value;
            else if (par is ParameterUshort parameterUshort)
                return parameterUshort.Value;
            else if (par is ParameterInt parameterInt)
                return parameterInt.Value;
            else if (par is ParameterUint parameterUint)
                return parameterUint.Value;
            else if (par is ParameterBool parameterBool)
                return parameterBool.Value ? 1 : 0;
            return 0;
        }

    }
}
