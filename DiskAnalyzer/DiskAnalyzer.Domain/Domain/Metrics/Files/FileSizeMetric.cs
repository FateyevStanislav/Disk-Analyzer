using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Library.Domain.Metrics.Files;

[MetricName("Общий размер файлов")]
public class FileSizeMetric : BaseMetric, IFileMetric
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