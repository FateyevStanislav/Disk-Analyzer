namespace DiskAnalyzer.Infrastructure;

public interface IRepository<TRecord> where TRecord : Record
{
    Task AddAsync(TRecord record, CancellationToken cancellationToken = default);
    Task<TRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TRecord>> GetAllOrderedAsync(
        bool descending = true, 
        CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default); 
}