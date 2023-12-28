using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
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
        private IEnumerable<Event>? _connectEvents;

        public Dictionary<Event, ParameterBase?> _eventsDictionary = new Dictionary<Event, ParameterBase?>();


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
            _comminicationService.ScanCompletedEvent += OnScanRtk;
            CreateConnectEvents();
            _parameterVm.CommunicationVm.PropertyChanged += OnRtkUnitsPropertyChanged;




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
            _eventsDictionary = new Dictionary<Event, ParameterBase?>();
            if (Events is null) return;

            foreach (var e in Events)
            {
                var rtk = _parameterVm.CommunicationVm.RtkUnits.Where(r => r == e.RtkUnit).FirstOrDefault();
                if (rtk != null && e.Parameter is not null)
                {
                    var par = rtk.Parameters.Where(p => p.Id == e.Parameter.Id).FirstOrDefault();
                    if (par != null)
                    {
                        _eventsDictionary.Add(e, par);
                    }
                }
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
            if (!(o is Event ev)) return;
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


        public void OnScanRtk()
        {
            foreach (var e in _eventsDictionary)
            {
                var @event = e.Key;
                var parameter = e.Value;
                if (@event is not null)
                {
                    if (parameter is not null && @event.RtkUnit is not null)
                    {
                        @event.IsActive = CompareCondition(@event, GetValueFromParameter(parameter));
                    }
                    else
                        @event.IsActive = false;

                }
            }
        }

        private bool CompareCondition(Event @event, float comparedValue)
        {
            switch (@event.EventCondition)
            {
                case EventCondition.Equal:
                    return comparedValue == @event.CompareValue;
                case EventCondition.NotEqual:
                    return comparedValue != @event.CompareValue;
                case EventCondition.LessThan:
                    return comparedValue < @event.CompareValue;
                case EventCondition.LessThanOrEqual:
                    return comparedValue <= @event.CompareValue;
                case EventCondition.GreaterThan:
                    return comparedValue > @event.CompareValue;
                case EventCondition.GreaterThanOrEqual:
                    return comparedValue >= @event.CompareValue;
                default:
                    return false; ;
            }
        }

        private float GetValueFromParameter(ParameterBase parameter)
        {
            if (parameter is ParameterShort parameterShort)
                return parameterShort.Value;
            else if (parameter is ParameterUshort parameterUshort)
                return parameterUshort.Value;
            else if (parameter is ParameterInt parameterInt)
                return parameterInt.Value;
            else if (parameter is ParameterUint parameterUint)
                return parameterUint.Value;
            else if (parameter is ParameterBool parameterBool)
                return parameterBool.Value ? 1 : 0;
            else if (parameter is ParameterFloat parameterFloat)
                return parameterFloat.Value;
            else if (parameter is ParameterDouble parameterDouble)
                return (float)parameterDouble.Value;
            return 0;
        }

        private void OnRtkUnitsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_parameterVm.CommunicationVm.RtkUnits))
            {
                CreateConnectEvents();
            }
        }


        private void CreateConnectEvents()
        {
            var events = _parameterVm.CommunicationVm.RtkUnits?.Select(r =>
            {
                var e = new Event() { UnVisisble = true, ActiveMessage = $"Нет связи", RtkUnit = r };
                r.PropertyChanged += (o, s) =>
                {
                    if (s.PropertyName == nameof(r.Connected))
                    {
                        e.IsActive =!r.Connected;                        
                    }
                };
                return e;
            }).ToList() ?? new List<Event>();
            if (_connectEvents is not null)
            {
                if (Events is not null)
                {
                    Events = Events.Except(_connectEvents).ToList();
                }
            }
            Events = Events?.Union(events).ToList();
            _connectEvents = events;
            if(Events is not null)
            {
                foreach (var e in Events)
                {
                    e.PropertyChanged += (o, a) => 
                    { 
                        if(a.PropertyName == nameof(e.IsActive) && e.IsActive)
                        {
                            e.LastActiveTime = DateTime.Now;
                        }
                    };
                }
            }
            
        }





    }



}
