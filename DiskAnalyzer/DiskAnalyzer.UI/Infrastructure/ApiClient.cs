using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Records.Grouping;
using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Infrastructure;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    public async Task<FilesMeasurementRecord> CreateMeasurementAsync(FilesMeasurementDto request)
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

        return await response.Content.ReadFromJsonAsync<FilesMeasurementRecord>(jsonOptions);
    }

    public async Task<FilesGroupingRecord> CreateGroupingAsync(GroupingMeasurementDto request)
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
        return await response.Content.ReadFromJsonAsync<FilesGroupingRecord>(jsonOptions);
    }
}