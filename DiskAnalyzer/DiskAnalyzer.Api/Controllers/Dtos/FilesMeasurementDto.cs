using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers.Dtos;

public record FilesMeasurementDto(
    [ExistingPath] string Path,
    [Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
    IEnumerable<FilesMeasurementType> MeasurementTypes,
    IEnumerable<FilterDto>? Filters);
