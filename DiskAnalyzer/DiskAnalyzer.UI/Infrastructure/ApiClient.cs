using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.UI.Infrastructure;
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

    public async Task<DirectoryMeasurementRecord> CreateMeasurementAsync(RequestDto request)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        jsonOptions.Converters.Add(new MetricJsonConverter());

        var response = await _httpClient.PostAsJsonAsync(
            "api/measurements/files",
            request,
            jsonOptions
        );

        response.EnsureSuccessStatusCode();

        try
        {
            var result = await response.Content.ReadFromJsonAsync<DirectoryMeasurementRecord>(jsonOptions);
            return result ?? throw new Exception("Deserialization returned null");
        }
        catch (JsonException ex)
        {
            throw new Exception($"JSON error at {ex.Path}: {ex.Message}");
        }
    }

    public async Task SaveToHistoryAsync()
    {
        var response = await _httpClient.PostAsync(
            "api/measurements/files/saveToHistory",
            null
        );

        response.EnsureSuccessStatusCode();
    }
}