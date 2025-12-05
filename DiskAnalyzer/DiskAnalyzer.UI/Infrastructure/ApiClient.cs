using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Domain.Records.Measurement;
using DiskAnalyzer.Infrastructure;
using System.Net.Http.Json;
using System.Text.Json;

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
}