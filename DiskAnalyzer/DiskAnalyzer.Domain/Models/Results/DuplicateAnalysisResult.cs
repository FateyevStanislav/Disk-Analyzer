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

    public string OldestOriginal { get; init; } = default!;

    public List<DuplicateGroup> DuplicateGroups { get; init; } = default!;

    [JsonConstructor]
    public DuplicateAnalysisResult(
        Guid Id,
        DateTime CreatedAt,
        string Path,
        IReadOnlyCollection<FilterInfo>? Filters,
        string WastedSpace,
        string OldestOriginal,
        List<DuplicateGroup> DuplicateGroups)
        : base(Id, CreatedAt, Path, Filters)
    {
        this.WastedSpace = WastedSpace;
        this.OldestOriginal = OldestOriginal;
        this.DuplicateGroups = DuplicateGroups;
    }

    public DuplicateAnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters,
        string wastedSpace,
        string oldestOriginal,
        List<DuplicateGroup> duplicateGroups)
        : base(path, filters)
    {
        WastedSpace = wastedSpace;
        OldestOriginal = oldestOriginal;
        DuplicateGroups = duplicateGroups;
    }
}