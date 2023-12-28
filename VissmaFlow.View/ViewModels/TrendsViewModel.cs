using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Extensions.Logging;

namespace VissmaFlow.View.ViewModels
{
    public class TrendsViewModel : ObservableObject
    {
        private readonly ILogger<TrendsViewModel> _logger;

        public TrendsViewModel(ILogger<TrendsViewModel> logger)
        {
            _logger = logger;
        }

        public ISeries[] Series { get; set; }
        = new ISeries[]
        {
            new LineSeries<double>
            {
               Values = new double[] {1,3,2,4,3,6,5,7,8 },
               Fill = null
             }
        };
    }
}
