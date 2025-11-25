using System.Collections.Concurrent;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Infrastructure.Repositories;

public class ConcDictRepository : IMeasurmentRecordRepository<Guid>
{
    private readonly ConcurrentDictionary<Guid, DirectoryMeasurementRecord> Repository = new();
    public int Count => Repository.Count;

    public void Add(DirectoryMeasurementRecord record)
    {
        if (!Repository.TryAdd(record.Id, record))
        {
            throw new ArgumentException("Exception in AddRecord Method");
        }
        Console.WriteLine($"Successfully added new {record.ToString()}");
    }

    public DirectoryMeasurementRecord Get(Guid id)
    {
        if (!Repository.TryGetValue(id, out DirectoryMeasurementRecord record))
        {
            throw new ArgumentException("Exception in GetRecord Method");
        }
        Console.WriteLine($"Successfully got value of {record.ToString()}");
        return record;
    }

    public bool Remove(Guid id)
    {
        if (!Repository.TryRemove(id, out DirectoryMeasurementRecord record))
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

    public IEnumerable<DirectoryMeasurementRecord> GetAllDescOrder()
    {
        return Repository
            .Values
            .OrderByDescending(wr => wr.CreatedAt);
    }

    public IEnumerable<DirectoryMeasurementRecord> GetAllAscOrder()
    {
        return Repository
            .Values
            .OrderBy(wr => wr.CreatedAt);
    }
}