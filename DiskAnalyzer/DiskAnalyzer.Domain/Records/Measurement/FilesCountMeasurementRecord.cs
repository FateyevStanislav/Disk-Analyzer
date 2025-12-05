using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.Measurement;

public record FilesCountMeasurementRecord(
    string Path,
    long FileCount,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);
