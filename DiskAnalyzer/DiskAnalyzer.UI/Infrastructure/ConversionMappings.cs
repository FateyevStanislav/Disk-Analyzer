using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Domain.Infrastructure.Filters;
using DiskAnalyzer.Domain.Metrics.Files;

namespace DiskAnalyzer.UI.Infrastructure;
public static class ConversionMappings
{
    public static Dictionary<Type, FilesMeasurementType> TypeToWeightingType = new Dictionary<Type, FilesMeasurementType>
    {
        { typeof(FileSizeMetric), FilesMeasurementType.Size},
        { typeof(FileCountMetric), FilesMeasurementType.Count}
    };

    public static Dictionary<Type, FilterType> TypeToFilterType = new Dictionary<Type, FilterType>
    {
        { typeof(ExtensionFilter), FilterType.Extension },
        { typeof(SizeFilter), FilterType.Size },
        { typeof(AccessTimeFilter), FilterType.AcessTime},
        { typeof(CreationTimeFilter), FilterType.CreationTime},
        { typeof(WriteTimeFilter), FilterType.WriteTime},
    };
};