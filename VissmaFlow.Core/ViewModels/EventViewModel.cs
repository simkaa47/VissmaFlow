using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Contracts.Events;
using VissmaFlow.Core.Models.Event;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        private readonly ILogger<EventViewModel> _logger;
        private readonly IRepository<Event> _eventRepository;
        private readonly IComminicationService _comminicationService;
        private readonly ParameterVm _parameterVm;
        private readonly IEventDialog _eventDialog;
        [ObservableProperty]
        private IEnumerable<Event>? _events;
        
        public List<ParameterBase> _eventPars = new List<ParameterBase>();


        public EventViewModel(ILogger<EventViewModel> logger, 
            IRepository<Event> eventRepository, 
            IComminicationService comminicationService, 
            ParameterVm parameterVm, IEventDialog eventDialog)
        {
            _logger = logger;
            this._eventRepository = eventRepository;
            _comminicationService = comminicationService;
            _parameterVm = parameterVm;
            _eventDialog = eventDialog;
            InitAsync();
            


        }

        private async void InitAsync()
        {
            try
            {
                Events = await _eventRepository.ListAllAsync();
                DecribeOnScanEvent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка инициализации событий:{ex.Message}");
            }
        }

        private void DecribeOnScanEvent()
        {
            if (_parameterVm.CommunicationVm.RtkUnits is null) return;
            foreach (var rtk in _parameterVm.CommunicationVm.RtkUnits)
            {
                //var pars = rtk.Parameters.SelectMany()
            }
        }


        #region Команды
        [RelayCommand]
        private async Task AddEventAsync()
        {
            var e = new Event();
            e = await _eventDialog.ShowDialog(e);
            if (e is null) return;
            try
            {
                _logger.LogInformation($"Выполняется добавление события \"{e.ActiveMessage}\"");
                await _eventRepository.AddAsync(e);
                InitAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Добавление события \"{e.ActiveMessage}\" {ex.Message}");
            }
        }  
       
        [RelayCommand]
        private async Task EditEventAsync(object o)
        {
            if (!(o is  Event ev)) return;            
            var e = await _eventDialog.ShowDialog(ev);
            if (e is null) return;
            try
            {
                _logger.LogInformation($"Выполняется изменение события \"{e.ActiveMessage}\"");
                await _eventRepository.UpdateAsync(e);                

            }
            catch (Exception ex)
            {
                _logger.LogError($"Изменение события \"{e.ActiveMessage}\" {ex.Message}");
            }
        }
        

        [RelayCommand]
        private async Task DeleteEventAsync(object o)
        {
            if (!(o is Event e)) return;
            try
            {
                _logger.LogInformation($"Выполняется удаление события \"{e.ActiveMessage}\"");
                await _eventRepository.DeleteAsync(e);
                InitAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Удаление события \"{e.ActiveMessage}\" {ex.Message}");
            }
        }
       

        [RelayCommand]
        private async Task SaveEventAsync()
        {
            if (Events is null) return;
            try
            {
                _logger.LogInformation($"Выполняется сохранение всех событий");
                await _eventRepository.UpdateAllAsync(Events.ToList());                

            }
            catch (Exception ex)
            {
                _logger.LogError($"Cохранение всех событий - {ex.Message}");
            }
        }
        #endregion

    }
}
