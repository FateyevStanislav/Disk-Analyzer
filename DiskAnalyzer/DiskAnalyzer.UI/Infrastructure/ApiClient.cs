using DiskAnalyzer.Api.Controllers.Dtos;
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

    public async Task<DuplicateAnalysisResult> FindDuplicatesAsync(DuplicateFinderDto request)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var response = await _httpClient.PostAsJsonAsync(
            "api/measurements/duplicates",
            request,
            jsonOptions
        );

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<DuplicateAnalysisResult>(jsonOptions);
        return result;
    }

    public async Task<bool> SaveToHistoryAsync(AnalysisResult request)
    {
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.PostAsJsonAsync(
                "api/history/append",
                request,
                jsonOptions
            );

            return response.IsSuccessStatusCode;   
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при сохранении в историю: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteFromHistoryAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/history/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при удалении из истории: {ex.Message}");
            return false;
        }
    }

    public async Task<IReadOnlyList<AnalysisResult>> GetRecordsFromHistoryAsync(bool descending = false)
    {
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.GetAsync($"api/history?descending={descending}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<AnalysisResult>>(jsonOptions);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при получении истории: {ex.Message}");
            return new List<AnalysisResult>().AsReadOnly();
        }
    }

    public async Task<AnalysisResult> GetHistoryRecordByIdAsync(Guid id)
    {
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.GetAsync($"api/history/{id}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AnalysisResult>(jsonOptions);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при получении записи по ID: {ex.Message}");
            return null;
        }
    }
}