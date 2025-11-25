using DiskAnalyzer.Library.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Library.Domain.Metrics;

public class GroupSizeMetric : BaseMetric
{
    public override string Name => "GroupSize";
    public string GroupKey { get; }
    private readonly long sizeInBytes;

    public GroupSizeMetric(long sizeInBytes, string groupKey)
        : base(new SizeFormatter())
    {
        this.sizeInBytes = sizeInBytes;
        GroupKey = groupKey;
    }

    protected override object RawValue => sizeInBytes;
}