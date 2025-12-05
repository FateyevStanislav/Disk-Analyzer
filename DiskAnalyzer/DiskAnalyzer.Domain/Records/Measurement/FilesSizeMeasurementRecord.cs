using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.Measurement;

public record FilesSizeMeasurementRecord(
    string Path,
    long TotalSize,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);