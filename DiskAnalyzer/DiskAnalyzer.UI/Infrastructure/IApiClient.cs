using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Domain.Models.Results;
public interface IApiClient
{
    // Основные методы анализа
    Task<MeasurementAnalysisResult> CreateMeasurementAsync(FilesMeasurementDto request);
    Task<GroupingAnalysisResult> CreateGroupingAsync(GroupingMeasurementDto request);
    Task<Dictionary<string, Dictionary<string, string>>> GetAvailableFiltersAsync();
    Task<DuplicateAnalysisResult> FindDuplicatesAsync(DuplicateFinderDto request);

    // Методы работы с историей
    Task<bool> SaveToHistoryAsync(AnalysisResult request);
    Task<bool> DeleteFromHistoryAsync(Guid id);
    Task<IReadOnlyList<AnalysisResult>> GetRecordsFromHistoryAsync(bool descending = false);
    Task<AnalysisResult> GetHistoryRecordByIdAsync(Guid id);
    //Task<int> GetHistoryCountAsync();
    //Task<bool> ClearHistoryAsync();
}