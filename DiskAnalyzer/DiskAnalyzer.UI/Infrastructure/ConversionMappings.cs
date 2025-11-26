using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.UI.Infrastructure
{
    public static class ConversionMappings
    {
        public static Dictionary<Type, WeightingType> TypeToWeightingType = new Dictionary<Type, WeightingType>
        {
            { typeof(FileSizeMetricType), WeightingType.Size},
            { typeof(FileCountMetricType), WeightingType.Count}
        };

        public static Dictionary<Type, FilterType> TypeToFilterType = new Dictionary<Type, FilterType>
        {
            { typeof(ExtensionFilter), FilterType.Extension },
            { typeof(SizeFilter), FilterType.Size }
        };
    }
}
