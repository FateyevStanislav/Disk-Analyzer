using DiskAnalyzer.Library.Domain;

public interface IMeasurmentRecordRepository<TId>
{
    void Add(MeasurmentRecord record);
    MeasurmentRecord Get(TId id);
    bool Remove(TId id);
    void Clear();
    int Count { get; }
    IEnumerable<MeasurmentRecord> GetAllDescOrder();
    IEnumerable<MeasurmentRecord> GetAllAscOrder();
}
