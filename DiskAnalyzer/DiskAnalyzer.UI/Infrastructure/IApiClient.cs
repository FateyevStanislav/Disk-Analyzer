using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Infrastructure;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiskAnalyzer.Domain.Models.Results;

public interface IApiClient
{
    Task<MeasurementAnalysisResult> CreateMeasurementAsync(FilesMeasurementDto request);
    Task<GroupingAnalysisResult> CreateGroupingAsync(GroupingMeasurementDto request);
    Task<Dictionary<string, Dictionary<string, string>>> GetAvailableFiltersAsync();
    Task<DuplicateAnalysisResult> FindDuplicatesAsync(DuplicateFinderDto request);
}