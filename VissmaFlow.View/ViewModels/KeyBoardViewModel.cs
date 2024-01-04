using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tmds.DBus.Protocol;

namespace VissmaFlow.View.ViewModels
{
    public partial class KeyBoardViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isOskVisible;


        public KeyBoardViewModel()
        {
            WeakReferenceMessenger.Default.Register<OskControlMsg>(this, (r, m) =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    IsOskVisible = m.Value;
                });
            });
        }
       
    }
}
