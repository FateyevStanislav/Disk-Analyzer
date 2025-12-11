using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Models.Results;
using System.Collections.Concurrent;

namespace DiskAnalyzer.Infrastructure.Repositories;

public class InMemoryRepository : IRepository
{
    private readonly ConcurrentDictionary<Guid, AnalysisResult> results = new();

    public Task AddAsync(AnalysisResult record, CancellationToken cancellationToken = default)
    {
        results[record.Id] = record;  
        return Task.CompletedTask;
    }

    public Task<AnalysisResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        results.TryGetValue(id, out var result);  
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<AnalysisResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<AnalysisResult>>([.. results.Values]);
    }

    public Task<IReadOnlyList<AnalysisResult>> GetAllOrderedAsync(
        bool descending = false,
        CancellationToken cancellationToken = default)
    {
        var ordered = descending
            ? results.Values.OrderByDescending(r => r.CreatedAt)
            : results.Values.OrderBy(r => r.CreatedAt);
        return Task.FromResult<IReadOnlyList<AnalysisResult>>([.. ordered]);
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(results.TryRemove(id, out _));  
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(results.Count); 
    }

    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        results.Clear();  
        return Task.CompletedTask;
    }
}
