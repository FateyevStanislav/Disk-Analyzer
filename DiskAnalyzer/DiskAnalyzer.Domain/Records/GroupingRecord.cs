using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records;

public class GroupingRecord : BaseRecord
{
    public GroupingRecord(
        Guid id,
        string path,
        IReadOnlyCollection<IMetric> metrics) : base(id)
    {
        Path = path;
        Metrics = metrics ?? Array.Empty<IMetric>();
    }

    public override string Path { get; init; }
    public override IReadOnlyCollection<IMetric> Metrics { get; init; }
}