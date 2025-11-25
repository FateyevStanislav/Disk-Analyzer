using DiskAnalyzer.Library.Domain.Records;

public interface IMeasurmentRecordRepository<TId>
{
    void Add(DirectoryMeasurmentRecord record);
    DirectoryMeasurmentRecord Get(TId id);
    bool Remove(TId id);
    void Clear();
    int Count { get; }
    IEnumerable<DirectoryMeasurmentRecord> GetAllDescOrder();
    IEnumerable<DirectoryMeasurmentRecord> GetAllAscOrder();
}
