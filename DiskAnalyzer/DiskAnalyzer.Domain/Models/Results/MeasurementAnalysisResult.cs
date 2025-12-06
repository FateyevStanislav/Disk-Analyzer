namespace DiskAnalyzer.Domain.Models.Results;

public sealed record MeasurementAnalysisResult(
    Guid Id,
    DateTime CreatedAt,
    string Path,
    IReadOnlyCollection<FilterInfo>? Filters,
    Dictionary<string, string> Metrics
) : AnalysisResult(Id, CreatedAt, Path, "FilesMeasurement", Filters);