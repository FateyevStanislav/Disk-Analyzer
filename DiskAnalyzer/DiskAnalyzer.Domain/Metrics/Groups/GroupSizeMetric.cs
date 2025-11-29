using DiskAnalyzer.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Domain.Metrics.Groups;

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