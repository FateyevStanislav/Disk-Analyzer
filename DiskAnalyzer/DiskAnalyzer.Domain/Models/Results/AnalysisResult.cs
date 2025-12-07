using System.Text.Json.Serialization;

namespace DiskAnalyzer.Domain.Models.Results;


/// <summary>
/// Базовый класс для всех типов результатов анализа файловой системы.
/// </summary>
/// <remarks>
/// Использует полиморфную JSON-сериализацию через <see cref="System.Text.Json"/>.
/// Дискриминатор: "analysisType" со значениями названий измерений
/// </remarks>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "analysisType")]
[JsonDerivedType(typeof(DuplicateAnalysisResult), "DuplicatesFinding")]
[JsonDerivedType(typeof(GroupingAnalysisResult), "FilesGrouping")]
[JsonDerivedType(typeof(MeasurementAnalysisResult), "FilesMeasurement")]
public abstract record AnalysisResult
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Path { get; init; }
    public IReadOnlyCollection<FilterInfo>? Filters { get; init; }

    [JsonConstructor]
    protected AnalysisResult(
        Guid id,
        DateTime createdAt,
        string path,
        IReadOnlyCollection<FilterInfo>? filters)
    {
        Id = id;
        CreatedAt = createdAt;
        Path = path;
        Filters = filters;
    }

    protected AnalysisResult(
        string path,
        IReadOnlyCollection<FilterInfo>? filters = null)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Path = path;
        Filters = filters;
    }
}