using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;

public class CombinedMeasurementStrategy : IFilesMeasurementStrategy
{
    public Record CreateRecord(string path, long fileCount, long totalSize,
                              IReadOnlyCollection<FilterInfo>? filters)
        => new FilesMeasurementRecord(path, fileCount, totalSize, filters);
}
