using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class ConversionsHandler : IConversionService
    {
        public WeightingType? ConvertMetricToWeightingType(Type metricType)
        {
            if (!typeof(IMetric).IsAssignableFrom(metricType))
                return null;

            return ConversionMappings.TypeToWeightingType.TryGetValue(metricType, out var result) ? result : null;
        }

        public FilterType? ConvertFilterToFilterType(Type filterType)
        {
            if (!typeof(IFileFilter).IsAssignableFrom(filterType))
                return null;

            return ConversionMappings.TypeToFilterType.TryGetValue(filterType, out var result) ? result : null;
        }
    }
}
