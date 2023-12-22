using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace VissmaFlow.Core.ViewModels
{
    public partial class ErrorsViewModel : ObservableObject
    {
        private readonly ILogger<ErrorsViewModel> _logger;

        public ErrorsViewModel(ILogger<ErrorsViewModel> logger)
        {
            _logger = logger;
        }
    }
}
