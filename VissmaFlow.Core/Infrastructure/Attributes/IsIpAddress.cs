using System.ComponentModel.DataAnnotations;

namespace VissmaFlow.Core.Infrastructure.Attributes
{
    public class IsIpAddress : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (!(value is string ip)) return false;
            var bytesStr = ip.Split(new char[] { '.' });
            if (bytesStr.Length != 4) return false;
            int num = 0;
            var bytes = bytesStr.Where(b => int.TryParse(b, out num))
                .Select(b => num).Where(b => b >= 0 && b <= 255);
            if (bytes.Count() == 4) return true;
            return false;

        }
    }
}
