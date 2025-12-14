using System.Text.Json.Serialization;

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
    public string WastedSpace { get; init; } = default!;

    public IReadOnlyList<DuplicateGroup> DuplicateGroups { get; init; } = default!;

    [JsonConstructor]
    public DuplicateAnalysisResult(
        Guid Id,
        DateTime CreatedAt,
        string Path,
        IReadOnlyCollection<FilterInfo>? Filters,
        string WastedSpace,
        IReadOnlyList<DuplicateGroup> DuplicateGroups)
        : base(Id, CreatedAt, Path, Filters)
    {
        this.WastedSpace = WastedSpace;
        this.DuplicateGroups = DuplicateGroups;
    }

    public DuplicateAnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters,
        string wastedSpace,
        IReadOnlyList<DuplicateGroup> duplicateGroups)
        : base(path, filters)
    {
        WastedSpace = wastedSpace;
        DuplicateGroups = duplicateGroups;
    }
}