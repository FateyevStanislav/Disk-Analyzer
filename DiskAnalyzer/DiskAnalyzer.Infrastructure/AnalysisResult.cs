using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Infrastructure;

public record AnalysisResult(
    Guid Id, 
    DateTime CreatedAt, 
    string Path, 
    string AnalysisType,
    Dictionary<string, string>? MetricsValues,
    IReadOnlyCollection<FilterInfo>? Filters,
    object? Details = null)
{
    public AnalysisResult(
        string path,
        string analysisType,
        Dictionary<string, string>? metricsValues,
        IReadOnlyCollection<FilterInfo>? filters,
        object? details = null) 
        : this(Guid.NewGuid(), DateTime.UtcNow, path, analysisType, metricsValues, filters, details)
    { }
}
