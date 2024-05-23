using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VissmaFlow.Core.Models.AccessControl
{
    public partial class Login : ObservableValidator
    {
        [ObservableProperty]        
        [Required(ErrorMessage ="Поле для логина не должно быть пустым")]
        [MinLength(3, ErrorMessage = "Логин должен содержать как минимум 3 символа")]
        private string? _loginName;

        [ObservableProperty]        
        [Required(ErrorMessage = "Поле для пароля не должно быть пустым")]
        [MinLength(3, ErrorMessage = "Пароль должен содержать как минимум 3 символа")]
        private string? _password;

        [ObservableProperty]
        private bool _faliledLogin;

        [ObservableProperty]
        private bool _isSuccessLogin;


        public Login()
        {
            this.PropertyChanged += (o, s) =>
            {
                if (s.PropertyName != nameof(FaliledLogin))
                {
                    FaliledLogin = false;
                }
            };
        }
    }
}
