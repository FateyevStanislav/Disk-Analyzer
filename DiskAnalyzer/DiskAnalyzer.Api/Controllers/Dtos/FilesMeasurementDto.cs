using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers.Dtos;

public record FilesMeasurementDto(
    [ExistingPath] string Path,
    [MinLength(1, ErrorMessage = "There must be least 1 measurement type")] IEnumerable<FilesMeasurementType> MeasurementTypes,
    [Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
    IEnumerable<FilterDto>? Filters);
