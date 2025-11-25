using System.Collections.Concurrent;
using DiskAnalyzer.Library.Domain;

namespace DiskAnalyzer.Library.Infrastructure.Repositories;

public class ConcDictRepository : IWeightingRecordRepository<Guid>
{
    private readonly ConcurrentDictionary<Guid, WeightingRecord> Repository = new();
    public int Count => Repository.Count;

    public void Add(WeightingRecord record)
    {
        if (!Repository.TryAdd(record.Id, record))
        {
            throw new ArgumentException("Exception in AddRecord Method");
        }
        Console.WriteLine($"Successfully added new {record.ToString()}");
    }

    public WeightingRecord Get(Guid id)
    {
        if (!Repository.TryGetValue(id, out WeightingRecord record))
        {
            throw new ArgumentException("Exception in GetRecord Method");
        }
        Console.WriteLine($"Successfully got value of {record.ToString()}");
        return record;
    }

    public bool Remove(Guid id)
    {
        if (!Repository.TryRemove(id, out WeightingRecord record))
        {
            return false;
            throw new ArgumentException("Exception in RemoveRecord Method");
        }
        Console.WriteLine($"Successfully removed {record.ToString()}");
        return true;
    }

    public void Clear()
    {
        Repository.Clear();
    }

    public IEnumerable<WeightingRecord> GetAllDescOrder()
    {
        return Repository
            .Values
            .OrderByDescending(wr => wr.CreatedAt);
    }

    public IEnumerable<WeightingRecord> GetAllAscOrder()
    {
        return Repository
            .Values
            .OrderBy(wr => wr.CreatedAt);
    }
}