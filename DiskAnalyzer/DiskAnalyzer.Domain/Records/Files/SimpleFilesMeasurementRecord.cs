using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records.Files;

public record SimpleFilesMeasurementRecord(
    Guid Id,
    string Path,
    MeasurementType Type,
    long Value,
    IReadOnlyCollection<FilterInfo>? Filters,
    DateTime CreatedAt) : IRecord;
