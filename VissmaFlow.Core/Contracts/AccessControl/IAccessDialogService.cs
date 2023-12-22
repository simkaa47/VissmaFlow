using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.Core.Contracts.AccessControl
{
    public interface IAccessDialogService
    {
        Task<bool> ShowDialog(User user);
    }
}
