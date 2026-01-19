namespace DiskAnalyzer.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T record);

    Task<T?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<T>> GetAllOrderedAsync(bool descending = false);

    Task<bool> RemoveAsync(Guid id);

    Task<int> CountAsync();

    Task ClearAsync();
}
