using System.Collections.Concurrent;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Infrastructure.Repositories;

public class ConcDictRepository : IMeasurmentRecordRepository<Guid>
{
    private readonly ConcurrentDictionary<Guid, DirectoryMeasurmentRecord> Repository = new();
    public int Count => Repository.Count;

    public void Add(DirectoryMeasurmentRecord record)
    {
        if (!Repository.TryAdd(record.Id, record))
        {
            throw new ArgumentException("Exception in AddRecord Method");
        }
        Console.WriteLine($"Successfully added new {record.ToString()}");
    }

    public DirectoryMeasurmentRecord Get(Guid id)
    {
        if (!Repository.TryGetValue(id, out DirectoryMeasurmentRecord record))
        {
            throw new ArgumentException("Exception in GetRecord Method");
        }
        Console.WriteLine($"Successfully got value of {record.ToString()}");
        return record;
    }

    public bool Remove(Guid id)
    {
        if (!Repository.TryRemove(id, out DirectoryMeasurmentRecord record))
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

    public IEnumerable<DirectoryMeasurmentRecord> GetAllDescOrder()
    {
        return Repository
            .Values
            .OrderByDescending(wr => wr.CreatedAt);
    }

    public IEnumerable<DirectoryMeasurmentRecord> GetAllAscOrder()
    {
        return Repository
            .Values
            .OrderBy(wr => wr.CreatedAt);
    }
}