namespace DiskAnalyzer.Domain.Models.Results;

/// <summary>
/// Результат группировки файлов по заданному критерию.
/// </summary>
/// <param name="GrouperType">
/// Название стратегии группировки (например, "Extension", "Size Range").
/// </param>
/// <param name="Metrics">
/// Плоский словарь метрик в формате "{GroupKey}_{MetricType}".
/// Пример: ".pdf_TotalSize", ".jpg_FilesCount".
/// </param>
/// <param name="Groups">
/// Структурированный список групп с файлами и метриками.
/// </param>
public sealed record GroupingAnalysisResult : AnalysisResult
{
    public string GrouperType { get; init; } = default!;
    public Dictionary<string, string> Metrics { get; init; } = default!;
    public List<FileGroup> Groups { get; init; } = default!;

    public GroupingAnalysisResult(
        Guid Id,
        DateTime CreatedAt,
        string Path,
        IReadOnlyCollection<FilterInfo>? Filters,
        string GrouperType,
        Dictionary<string, string> Metrics,
        List<FileGroup> Groups)
        : base(Id, CreatedAt, Path, "FilesGrouping", Filters)
    {
        this.GrouperType = GrouperType;
        this.Metrics = Metrics;
        this.Groups = Groups;
    }

    public GroupingAnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters,
        string grouperType,
        Dictionary<string, string> metrics,
        List<FileGroup> groups)
        : base(path, "FilesGrouping", filters)
    {
        GrouperType = grouperType;
        Metrics = metrics;
        Groups = groups;
    }
}