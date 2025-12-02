using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Domain.Records;

namespace DiskAnalyzer.UI.Infrastructure
{
    public interface IApiClient
    {
        Task<DirectoryMeasurementRecord> CreateMeasurementAsync(RequestDto request);
        Task SaveToHistoryAsync();
    }
}
