using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.Library.Domain.Records;

public class DirectoryMeasurmentRecord : BaseRecord
{
    public DirectoryMeasurmentRecord(
        Guid id, 
        string path, 
        IReadOnlyCollection<string>? logs, 
        IReadOnlyCollection<IMetric> metrics) : base(id)
    {
        Path = path;
        Logs = logs ?? Array.Empty<string>();
        Metrics = metrics ?? Array.Empty<IMetric>();
    }

    public override string Path { get; init; }
    public override IReadOnlyCollection<string> Logs { get; init; }
    public override IReadOnlyCollection<IMetric> Metrics { get; init; }
}