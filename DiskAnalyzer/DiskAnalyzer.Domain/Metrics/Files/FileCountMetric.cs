using DiskAnalyzer.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Domain.Metrics.Files;

public class FileCountMetric : BaseMetric
{
    public override string Name => "FileCount";

    private readonly int count;

    public FileCountMetric(int count)
        : base(new CountFormatter())
    {
        this.count = count;
    }

    protected override object RawValue => count;
}