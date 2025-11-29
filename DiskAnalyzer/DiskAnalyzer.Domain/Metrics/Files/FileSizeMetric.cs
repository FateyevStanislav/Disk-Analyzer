using DiskAnalyzer.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Domain.Metrics.Files;

public class FileSizeMetric : BaseMetric
{
    public override string Name => "FileSize";

    private readonly long sizeInBytes;

    public FileSizeMetric(long sizeInBytes)
        : base(new SizeFormatter())
    {
        this.sizeInBytes = sizeInBytes;
    }

    protected override object RawValue => sizeInBytes;
}