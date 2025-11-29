namespace DiskAnalyzer.Infrastructure;

public interface IRepository
{
    void Add(Record record);
    Record Get(Guid id);
    bool Remove(Guid id);
    void Clear();
    int Count { get; }
    IEnumerable<Record> GetAllDescOrder();
    IEnumerable<Record> GetAllAscOrder();
}