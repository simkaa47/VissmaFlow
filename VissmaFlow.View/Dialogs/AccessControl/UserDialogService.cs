﻿using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using VissmaFlow.Core.Contracts.AccessControl;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.Dialogs.AccessControl
{
    internal class UserDialogService : IAccessDialogService
    {
        public async Task<bool> ShowDialog(User user)
        {
            
            if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                UserDialogWindow userWindow = new UserDialogWindow(user);
                await userWindow.ShowDialogAsync();
                return userWindow.DialogResult;
            }
            return false;
        }
    }
}
