using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records;

public record FilesMeasurementRecord(
    string Path,
    long FileCount,
    long TotalSize,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);