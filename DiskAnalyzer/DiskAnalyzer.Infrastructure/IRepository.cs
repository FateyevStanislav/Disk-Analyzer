namespace DiskAnalyzer.Infrastructure;

public interface IRepository<TId>
{
    void Add(IRecord record);
    IRecord Get(TId id);
    bool Remove(TId id);
    void Clear();
    int Count { get; }
    IEnumerable<IRecord> GetAllDescOrder();
    IEnumerable<IRecord> GetAllAscOrder();
}