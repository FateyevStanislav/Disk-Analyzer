using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.Measurement;

public record FilesMeasurementRecord(
    string Path,
    long FileCount,
    long TotalSize,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);
