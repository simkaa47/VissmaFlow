using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Contracts.Communication
{
    public interface IRtkUnitDialog
    {
        Task<RtkUnit?> ShowDialog();
    }
}
