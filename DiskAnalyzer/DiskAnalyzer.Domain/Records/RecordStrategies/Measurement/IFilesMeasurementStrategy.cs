using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;

public interface IFilesMeasurementStrategy
{
    Record CreateRecord(string path, long fileCount, long totalSize,
                       IReadOnlyCollection<FilterInfo>? filters);
}