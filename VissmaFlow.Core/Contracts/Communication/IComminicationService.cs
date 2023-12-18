using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Contracts.Communication
{
    public interface IComminicationService
    {
        public void ReadAllData(IEnumerable<ParameterBase>? parameters, int unitId);
        public void WriteParameter(ParameterBase parameter);

        public event Action ScanCompletedEvent;
        public bool Connected { get; }
    }
}
