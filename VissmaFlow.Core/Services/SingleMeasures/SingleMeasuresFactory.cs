using VissmaFlow.Core.Models.SingleMeasures;

namespace VissmaFlow.Core.Services.SingleMeasures
{
    internal static  class SingleMeasuresFactory
    {
        public static List<SingleMeasureSettings> SeedAsync()
        {
            return new List<SingleMeasureSettings>()
            {
                new SingleMeasureSettings()
                {
                    Duration = 30,
                    
                }
            };
        }
    }
}
