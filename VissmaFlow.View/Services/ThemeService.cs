using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Models.Administration;

namespace VissmaFlow.View.Services
{
    public class ThemeService
    {
        private readonly IRepository<PcSettings> _repository;
        private readonly ILogger<ThemeService> _logger;

        public ThemeService(IRepository<PcSettings> repository, ILogger<ThemeService> logger)
        {
            _repository = repository;
            _logger = logger;
            InitAsync();
        }

        private PcSettings? _settings;

        private async void InitAsync()
        {
            try
            {
                _settings = (await _repository.ListAllAsync()).FirstOrDefault();
                var app = App.Current as App;
                if (app != null && _settings is not null)
                {
                    if(app.ActualThemeVariant == ThemeVariant.Light && _settings.Theme == Themes.Dark||
                       app.ActualThemeVariant == ThemeVariant.Dark && _settings.Theme == Themes.Light)
                    {
                        await ChangeThemeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Инициализация темы из настроек - {ex.Message}");
            }
        }

        

        public async Task ChangeThemeAsync()
        {
            var app = App.Current as App;
            if (app != null && _settings is not null)
            {
                if (app.RequestedThemeVariant == ThemeVariant.Dark)
                {
                    app.RequestedThemeVariant = ThemeVariant.Light;
                    _settings.Theme = Themes.Light;
                }
                else
                {
                    app.RequestedThemeVariant = ThemeVariant.Dark;
                    _settings.Theme = Themes.Dark;
                }
                try
                {
                    await _repository.UpdateAsync(_settings);
                }
                catch (Exception)
                {

                }

            }
        }


    }
}
