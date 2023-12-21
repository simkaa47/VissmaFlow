using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using VissmaFlow.Core.Contracts.Common;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Infrastructure.AccessControl;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.Services.AccessControl;

namespace VissmaFlow.Core.ViewModels
{
    public partial class AccessViewModel : ObservableObject
    {
        private readonly ILogger<AccessViewModel> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IQuestionDialog _questionDialog;
        private readonly IAccessDialogService _accessDialogService;
        [ObservableProperty]
        private IEnumerable<User>? _users;
        [ObservableProperty]
        private User? _currentUser;

        public AccessViewModel(ILogger<AccessViewModel> logger, 
            IRepository<User> userRepository, 
            IQuestionDialog questionDialog, 
            IAccessDialogService accessDialogService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _questionDialog = questionDialog;
            _accessDialogService = accessDialogService;
            InitAsync();
        }


        private async void InitAsync()
        {
            try
            {
                _logger.LogInformation($"Инициализация данных пользователей");
                Users = await _userRepository.InitAsync(UserDataFactory.GetUsers(), 3);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при  инициализации данных пользователей - {ex.Message}");
            }
        }

        #region Команды
        [RelayCommand]
        private async Task AddUserAsync()
        {
            var newUser = new User();
            if (!await _accessDialogService.ShowDialog(newUser)) return;
            try
            {
                if (newUser.HasErrors)
                {
                    throw new Exception("New user adding: validation error");
                }
                _logger.LogInformation($"Выполняется добавление пользователя {newUser.FullName}");
                await _userRepository.AddAsync(newUser);
                Users = await _userRepository.ListAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления пользователя {newUser.FullName} - {ex.Message}");
            }
        }




        [RelayCommand]
        private async Task UpdateUserAsync(object parameter)
        {
            if (!(parameter is User user)) return;
            _logger.LogInformation($"Выполняется редактирование пользователя {user.FullName}");
            if (!await _accessDialogService.ShowDialog(user)) return;
            try
            {
                if (user.HasErrors)
                {
                    throw new Exception("New user adding: validation error");
                }

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка редактирования пользователя {user.FullName} - {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task DeleteUserAsync(object parameter)
        {
            if (!(parameter is User user)) return;
            try
            {
                if (await _questionDialog.Ask($"Удаление пользователя {user.FullName}", $"Вы действительно хотите Удалить пользователя {user.FullName}?"))
                {
                    _logger.LogInformation($"Выполняется удаление пользователя {user.FullName}");
                    await _userRepository.DeleteAsync(user);
                    Users = await _userRepository.ListAllAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка удаления пользователя {user.FullName} - {ex.Message}");
            }

        }


        [RelayCommand]
        public async Task LoginAsync(object parameter)
        {
            if (!(parameter is Login login)) return;
            try
            {
                var user = await _userRepository.GetFirstWhere(u => u.Login == login.LoginName && u.Password == login.Password);
                if (user == null)
                {
                    login.FaliledLogin = true;
                    _logger.LogInformation($"Попытка авторизоваться с логином = {login.LoginName} и паролем {login.Password} была неуспешной");
                }
                else
                {
                    CurrentUser = user;
                    _logger.LogInformation($"Пользователь c логином {login.LoginName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при авторизации пользователя с логином  {login.LoginName} - {ex.Message}");
            }
        }

        [RelayCommand]
        public void Logout()
        {
            CurrentUser = null;
        } 
        #endregion
    }
}
