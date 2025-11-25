using DiskAnalyzer.Library.Domain;

public interface IWeightingRecordRepository<TId>
{
    void Add(WeightingRecord record);
    WeightingRecord Get(TId id);
    bool Remove(TId id);
    void Clear();
    int Count { get; }
    IEnumerable<WeightingRecord> GetAllDescOrder();
    IEnumerable<WeightingRecord> GetAllAscOrder();
}
