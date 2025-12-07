using System.Text.Json.Serialization;

namespace DiskAnalyzer.Domain.Models.Results;

/// <summary>
/// Результат сбора метрик файловой системы без группировки.
/// </summary>
/// <param name="Measurements">
/// Словарь метрик: ключ = тип измерения, значение = результат.
/// Примеры: { "TotalSize": "104857600", "FilesCount": "42" }
/// </param>
public sealed record MeasurementAnalysisResult : AnalysisResult
{
    public Dictionary<string, string> Measurements { get; init; } = default!;

    [JsonConstructor]
    public MeasurementAnalysisResult(
        Guid Id,
        DateTime CreatedAt,
        string Path,
        IReadOnlyCollection<FilterInfo>? Filters,
        Dictionary<string, string> Measurements)
        : base(Id, CreatedAt, Path, Filters)
    {
        this.Measurements = Measurements;
    }

    public MeasurementAnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters,
        Dictionary<string, string> measurements)
        : base(path, filters)
    {
        Measurements = measurements;
    }
}