using System.Text.Json.Serialization;

namespace DiskAnalyzer.Domain.Models.Results;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "analysisType")]
[JsonDerivedType(typeof(DuplicateAnalysisResult), "DuplicatesFinding")]
[JsonDerivedType(typeof(GroupingAnalysisResult), "FilesGrouping")]
[JsonDerivedType(typeof(MeasurementAnalysisResult), "FilesMeasurement")]
public abstract record AnalysisResult(
    Guid Id,
    DateTime CreatedAt,
    string Path,
    string AnalysisType,
    IReadOnlyCollection<FilterInfo>? Filters = null);