using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.Core.Infrastructure.AccessControl
{
    public interface IAccessDialogService
    {
        Task<bool> ShowDialog(User user);
    }
}
