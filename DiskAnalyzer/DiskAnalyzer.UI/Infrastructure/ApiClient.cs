using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Infrastructure;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiskAnalyzer.Domain.Models.Results;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5122");
    }

    public async Task<Dictionary<string, Dictionary<string, string>>> GetAvailableFiltersAsync()
    {
        var response = await _httpClient.GetAsync("api/requestInfo/filters");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Dictionary<string, Dictionary<string, string>>>();
    }

    public async Task<MeasurementAnalysisResult> CreateMeasurementAsync(FilesMeasurementDto request)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var response = await _httpClient.PostAsJsonAsync(
            "api/measurements/files",
            request,
            jsonOptions
        );

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<MeasurementAnalysisResult>(jsonOptions);
    }

    public async Task<GroupingAnalysisResult> CreateGroupingAsync(GroupingMeasurementDto request)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var response = await _httpClient.PostAsJsonAsync(
            "api/measurements/groups",
            request,
            jsonOptions
        );

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<GroupingAnalysisResult>(jsonOptions);
        return result;
    }
}