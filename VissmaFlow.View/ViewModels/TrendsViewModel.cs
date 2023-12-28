using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
