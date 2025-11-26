using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers.Filters;

namespace DiskAnalyzer.UI.Infrastructure
{
    public interface IConversionService
    {
        FilesMeasurementType ConvertMetricToMeasurementType(Type metricType);
        FilterType ConvertFilterToFilterType(Type filterType);
    }
}
