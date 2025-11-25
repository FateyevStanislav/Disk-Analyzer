using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.UI.Infrastructure
{
    public static class ConversionsHandler
    {
        private static Dictionary<Type, WeightingType> TypeToWeightingType = new Dictionary<Type, WeightingType>
        {
            { typeof(FileSizeMetricType), WeightingType.Size},
            { typeof(FileCountMetricType), WeightingType.Count}
        };

        private static Dictionary<Type, FilterType> TypeToFilterType = new Dictionary<Type, FilterType>
        {
            { typeof(ExtensionFilter), FilterType.Extension },
            { typeof(SizeFilter), FilterType.Size }
        };

        public static WeightingType? ConvertMetricToWeightingType(Type type)
        {
            return TypeToWeightingType.TryGetValue(type, out var result)
                   ? result
                   : null;
        }

        public static FilterType? ConvertFilterToFilterType(Type type)
        {
            return TypeToFilterType.TryGetValue(type, out var result)
                   ? result
                   : null;
        }
    }
}
