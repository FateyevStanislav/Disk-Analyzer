using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.UI.Infrastructure;
using System.Net.Http.Json;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DirectoryMeasurementRecord> CreateMeasurementAsync(RequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/measurements/files",
            request
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<DirectoryMeasurementRecord>();
        return result ?? throw new Exception("Failed to deserialize response");
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