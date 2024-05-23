using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.View.ViewModels
{
    public partial class TrendsViewModel : ObservableObject
    {
        private readonly ILogger<TrendsViewModel> _logger;
        public TrendSettigsViewModel TrendSettigsViewModel { get; set; }
        Timer? _timer;

        public TrendsViewModel(ILogger<TrendsViewModel> logger, TrendSettigsViewModel trendSettigsViewModel)
        {
            _logger = logger;
            TrendSettigsViewModel = trendSettigsViewModel;
            InitAsync();
        }

        public List<ZoomAndPanMode> ZoomModes { get; set; } = new List<ZoomAndPanMode>
        {
            ZoomAndPanMode.None,
            ZoomAndPanMode.PanX,
            ZoomAndPanMode.PanY,
            ZoomAndPanMode.ZoomX,
            ZoomAndPanMode.ZoomY,
            ZoomAndPanMode.Both
        };
        [ObservableProperty]
        private ZoomAndPanMode _selectedZoomMode;

        private int _selectedZoomIndex = 0;
        public int SelectedZoomIndex
        {
            get => _selectedZoomIndex;
            set
            {
                SetProperty(ref _selectedZoomIndex, value);
                if (value< ZoomModes.Count)
                {
                    
                    SelectedZoomMode = ZoomModes[value];
                }
            }
        }

        private void InitAsync()
        {
            if (TrendSettigsViewModel.Curves is null) return;
            Series = TrendSettigsViewModel
                .Curves
                .Where(c => c.Parameter is not null && c.RtkUnit is not null)
                .Select(c => new LineSeries<DateTimePoint>
                {
                    Name = $"{c.RtkUnit?.Name} : {c.Parameter?.Description}",
                    Values = c.Values,                    
                    IsVisibleAtLegend = c.IsVisible,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    IsVisible = c.IsVisible,
                    Fill = null,
                    Stroke = new SolidColorPaint(GetSKColor(c.Color)) { StrokeThickness = 2 }

                }).ToArray();
            SetTimer();
            if (TrendSettigsViewModel.TrendSettings is not null)
            {
                TrendSettigsViewModel.TrendSettings.PropertyChanged += (o, args) =>
                {
                    if (args.PropertyName == nameof(TrendSettigsViewModel.TrendSettings.ScanFrequence))
                    {
                        SetTimer();
                    }
                };
            }
        }


        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            Fill = new SolidColorPaint(GetSKColor("#2C2C2E")),
            Stroke = new SolidColorPaint(SKColors.White.WithAlpha(128))
        };



        public object Sync { get; } = new object();

        public ISeries[]? Series { get; set; }

        public Axis[] YAxes { get; set; } =
        {
            new Axis()
            {
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                ShowSeparatorLines = true,
                Padding = new Padding(32, 5, 16, 5),
                LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
                SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
            }
        };

        public LiveChartsCore.Measure.Margin Margin { get; set; } = new LiveChartsCore.Measure.Margin(16);

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                ShowSeparatorLines = true,
                Padding = new Padding(16, 32, 16, 32),
                LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
                LabelsRotation = 90,                
                Labeler = (d)=>{
                    var ticks = (long)d;
                    if(ticks>0)
                    {
                        var dt = new DateTime(ticks);
                        return dt.ToString("HH:mm:ss");
                    }
                    return d.ToString();
                },
                SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
            }
        };

        private static double[] GetSeparators(Axis axis = null)
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
            return date.ToString("G");
        }




        private SKColor GetSKColor(string colorString)
        {
            var color = Color.Parse(colorString);
            var skColor = new SKColor(color.R, color.G, color.B, color.A);
            return skColor;
        }

        [RelayCommand]
        private void EnableRealTime()
        {
            if (XAxes is not null && XAxes.Length > 0)
            {
                XAxes[0].MaxLimit = null;
                XAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
                YAxes[0].MinLimit = null;
            }
        }


        private void OnTimer(object? o)
        {
            if (TrendSettigsViewModel.Curves is null) return;
            foreach (var c in TrendSettigsViewModel.Curves)
            {
                if (c.RtkUnit is not null && c.Parameter is not null && c.RtkUnit.Connected)
                {
                    var par = c.RtkUnit.Parameters.Where(p => p.Id == c.Parameter.Id).FirstOrDefault();
                    if (par is not null)
                    {
                        lock (Sync)
                        {
                            c.Values.Add(new DateTimePoint(DateTime.Now, GetValueFromParameter(par)));
                            var sett = TrendSettigsViewModel.TrendSettings;
                            if (sett is not null)
                            {
                                while (c.Values.Count > 0 && c.Values[0].DateTime < DateTime.Now.AddSeconds(sett.MaxTimeSeconds * (-1)))
                                {
                                    c.Values.RemoveAt(0);
                                }
                            }
                            //XAxes[0].CustomSeparators = GetSeparators(XAxes[0]); 
                        }
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
            else if (par is ParameterShort parameterShort)
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


        private void SetTimer()
        {
            var sett = TrendSettigsViewModel.TrendSettings;
            var interval = (sett is not null && sett.ScanFrequence >= 100) ? sett.ScanFrequence : 1000;
            if (_timer is null)
                _timer = new Timer(OnTimer);
            _timer.Change(0, interval);
        }

    }
}
