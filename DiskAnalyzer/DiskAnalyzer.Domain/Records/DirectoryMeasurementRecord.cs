using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records;

public class DirectoryMeasurementRecord : BaseRecord
{
    public DirectoryMeasurementRecord(
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