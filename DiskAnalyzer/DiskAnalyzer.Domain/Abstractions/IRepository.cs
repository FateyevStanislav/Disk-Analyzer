using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Abstractions;

public interface IRepository
{
    Task AddAsync(AnalysisResult record, CancellationToken cancellationToken = default);
    Task<AnalysisResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AnalysisResult>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AnalysisResult>> GetAllOrderedAsync(
        bool descending = true,
        CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default);
}