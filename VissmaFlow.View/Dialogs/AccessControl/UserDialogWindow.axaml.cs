using Avalonia.Controls;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.Dialogs.AccessControl;

public partial class UserDialogWindow : DialogWindow
{
    public UserDialogWindow()
    {
        InitializeComponent();
    }

    public UserDialogWindow(User user)
    {
        InitializeComponent();
        this.DataContext = user;
    }
}