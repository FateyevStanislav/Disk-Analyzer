using DiskAnalyzer.Library.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Library.Domain.Metrics.Groups;

public class GroupCountMetric : BaseMetric
{
    public override string Name => "GroupCount";
    public string GroupKey { get; }
    private readonly int count;

    public GroupCountMetric(int count, string groupKey)
        : base(new CountFormatter())
    {
        this.count = count;
        GroupKey = groupKey;
    }

    protected override object RawValue => count;
}