using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Domain.Records.Grouping;

public interface IApiClient
{
    Task<FilesMeasurementRecord> CreateMeasurementAsync(FilesMeasurementDto request);
    Task<FilesGroupingRecord> CreateGroupingAsync(GroupingMeasurementDto request);
    Task<Dictionary<string, Dictionary<string, string>>> GetAvailableFiltersAsync();
}

public record FilesMeasurementDto(
    FilesMeasurementStrategyType StrategyType,
    string Path,
    int MaxDepth,
    IEnumerable<FilterDto>? Filters);