using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records.Groups;

public record SimpleGroupsMeasurementRecord(
    Guid Id,
    string Path,
    MeasurementType Type,
    long Value,
    IReadOnlyCollection<FilterInfo>? Filters,
    DateTime CreatedAt) : IRecord;
