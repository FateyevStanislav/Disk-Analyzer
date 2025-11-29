using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records.Groups;

public record CombinedGroupsMeasurementRecord(
    Guid Id,
    string Path,
    long FileCount,
    long TotalSizeBytes,
    IReadOnlyCollection<FilterInfo>? Filters,
    DateTime CreatedAt) : IRecord;