using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records.Files;

public record CombinedFilesMeasurementRecord(
    Guid Id,
    string Path,
    long FileCount,
    long TotalSizeBytes,
    IReadOnlyCollection<FilterInfo>? Filters,
    DateTime CreatedAt) : IRecord;