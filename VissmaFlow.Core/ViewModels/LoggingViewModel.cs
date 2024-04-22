using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace VissmaFlow.Core.ViewModels
{
    public class LoggingViewModel : ObservableObject
    {
        private readonly ILogger<LoggingViewModel> _logger;

        public LoggingViewModel(ILogger<LoggingViewModel> logger)
        {
            _logger = logger;
        }
    }
}
