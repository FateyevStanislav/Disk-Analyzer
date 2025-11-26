using DiskAnalyzer.Library.Domain.Records;

public interface IMeasurmentRecordRepository<TId>
{
    void Add(DirectoryMeasurementRecord record);
    DirectoryMeasurementRecord Get(TId id);
    bool Remove(TId id);
    void Clear();
    int Count { get; }
    IEnumerable<DirectoryMeasurementRecord> GetAllDescOrder();
    IEnumerable<DirectoryMeasurementRecord> GetAllAscOrder();
}
