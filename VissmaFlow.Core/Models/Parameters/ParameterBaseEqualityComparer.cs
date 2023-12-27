using System.Diagnostics.CodeAnalysis;

namespace VissmaFlow.Core.Models.Parameters
{
    public class ParameterBaseEqualityComparer : IEqualityComparer<ParameterBase?>
    {
        public bool Equals(ParameterBase? x, ParameterBase? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null) return false;
            return x.Id == y.Id && x.Description == y.Description;

        }

        public int GetHashCode([DisallowNull] ParameterBase obj)
        {
            return obj.Id * obj.Id;
        }
    }
}
