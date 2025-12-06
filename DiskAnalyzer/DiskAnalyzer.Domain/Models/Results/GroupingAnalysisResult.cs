namespace DiskAnalyzer.Domain.Models.Results;

public sealed record GroupingAnalysisResult(
    Guid Id,
    DateTime CreatedAt,
    string Path,
    IReadOnlyCollection<FilterInfo>? Filters,
    string GrouperType,
    Dictionary<string, string> Metrics,
    List<FileGroup> Groups
) : AnalysisResult(Id, CreatedAt, Path, "FilesGrouping", Filters);
