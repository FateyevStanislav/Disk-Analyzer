using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain;

namespace DiskAnalyzer.UI.Infrastructure
{
    public interface IConversionService
    {
        WeightingType? ConvertMetricToWeightingType(Type metricType);
        FilterType? ConvertFilterToFilterType(Type filterType);
    }
}
