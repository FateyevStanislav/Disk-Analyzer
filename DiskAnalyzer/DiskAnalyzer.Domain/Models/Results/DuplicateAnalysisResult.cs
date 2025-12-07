namespace DiskAnalyzer.Domain.Models.Results;

/// <summary>
/// Результат поиска дублирующихся файлов.
/// </summary>
/// <remarks>
/// Дубликаты определяются по содержимому (SHA256 хеш).
/// TotalWastedSpace = сумма размеров всех файлов-дубликатов (минус оригиналы).
/// </remarks>
public sealed record DuplicateAnalysisResult : AnalysisResult
{
    public Dictionary<string, string> Metrics { get; init; } = default!;
    public List<DuplicateGroup> DuplicateGroups { get; init; } = default!;

    public DuplicateAnalysisResult(
        Guid Id,
        DateTime CreatedAt,
        string Path,
        IReadOnlyCollection<FilterInfo>? Filters,
        Dictionary<string, string> Metrics,
        List<DuplicateGroup> DuplicateGroups)
        : base(Id, CreatedAt, Path, "DuplicatesFinding", Filters)
    {
        this.Metrics = Metrics;
        this.DuplicateGroups = DuplicateGroups;
    }

    public DuplicateAnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters,
        Dictionary<string, string> metrics,
        List<DuplicateGroup> duplicateGroups)
        : base(path, "DuplicatesFinding", filters)
    {
        Metrics = metrics;
        DuplicateGroups = duplicateGroups;
    }
}