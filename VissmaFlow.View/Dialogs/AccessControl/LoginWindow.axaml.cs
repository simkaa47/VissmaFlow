using Avalonia.Interactivity;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.View.Dialogs.AccessControl;

public partial class LoginWindow : DialogWindow
{
    public LoginWindow()
    {
        InitializeComponent();
        var login = new Login();
        Login = login;
        DataContext = login;
    }
    
    Login Login { get; set; }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {        
        if (Login != null)
        {
            if (App.Current is App app )
            {
                var vm = app.GetService<AccessViewModel>();
                if (vm != null)
                {
                    await vm.LoginAsync(Login);
                    if (vm.CurrentUser != null)
                    {
                        needToCloseDialog = true;
                    }
                }
            }
        }
    }





}