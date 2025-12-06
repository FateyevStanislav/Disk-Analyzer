namespace DiskAnalyzer.Domain.Models.Results;

public sealed record DuplicateAnalysisResult(
    Guid Id,
    DateTime CreatedAt,
    string Path,
    IReadOnlyCollection<FilterInfo>? Filters,
    Dictionary<string, string> Metrics,
    List<DuplicateGroup> DuplicateGroups
) : AnalysisResult(Id, CreatedAt, Path, "DuplicatesFinding", Filters);
