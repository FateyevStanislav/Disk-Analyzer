using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class ConversionsHandler : IConversionService
    {
        public FilesMeasurementType ConvertMetricToMeasurementType(Type metricType)
        {
            if (!typeof(IMetric).IsAssignableFrom(metricType))
                throw new ArgumentException($"Type {metricType} does not implement IMetric");

            return ConversionMappings.TypeToWeightingType[metricType];
        }

        public FilterType ConvertFilterToFilterType(Type filterType)
        {
            if (!typeof(IFileFilter).IsAssignableFrom(filterType))
                throw new ArgumentException($"Type {filterType} does not implement IFileFilter");

            return ConversionMappings.TypeToFilterType[filterType];
        }
    }
}
