using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;

public class CountMeasurementStrategy : IFilesMeasurementStrategy
{
    public Record CreateRecord(string path, long fileCount, long totalSize,
                              IReadOnlyCollection<FilterInfo>? filters)
        => new FilesCountMeasurementRecord(path, fileCount, filters);
}
