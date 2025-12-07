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
public abstract record AnalysisResult(
    Guid Id,
    DateTime CreatedAt,
    string Path,
    string AnalysisType,
    IReadOnlyCollection<FilterInfo>? Filters = null)
{
    protected AnalysisResult(
        string path,
        string analysisType,
        IReadOnlyCollection<FilterInfo>? filters = null)
        : this(Guid.NewGuid(), DateTime.UtcNow, path, analysisType, filters) { }
}