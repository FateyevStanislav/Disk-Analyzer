using System.Collections.Concurrent;

namespace DiskAnalyzer.Infrastructure.Repositories;

public class InMemoryRepository<T>(Func<T, Guid> getIdFunc, Func<T, DateTime> getDateFunc) : IRepository<T> where T : class
{
    private readonly ConcurrentDictionary<Guid, T> storage = new();

    public Task AddAsync(T record)
    {
        var id = getIdFunc(record);
        storage[id] = record;
        return Task.CompletedTask;
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        storage.TryGetValue(id, out var result);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<T>> GetAllOrderedAsync(bool descending = false)
    {
        var ordered = descending
            ? storage.Values.OrderByDescending(getDateFunc)
            : storage.Values.OrderBy(getDateFunc);

        return Task.FromResult<IReadOnlyList<T>>([.. ordered]);
    }

    public Task<bool> RemoveAsync(Guid id)
    {
        return Task.FromResult(storage.TryRemove(id, out _));
    }

    public Task<int> CountAsync()
    {
        return Task.FromResult(storage.Count);
    }

    public Task ClearAsync()
    {
        storage.Clear();
        return Task.CompletedTask;
    }
}
