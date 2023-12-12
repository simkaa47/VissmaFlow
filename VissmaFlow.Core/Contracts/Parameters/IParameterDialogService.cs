using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Contracts.Parameters
{
    public interface IParameterDialogService
    {
        Task<ParameterBase?> ShowDialog();
        
    }
}
