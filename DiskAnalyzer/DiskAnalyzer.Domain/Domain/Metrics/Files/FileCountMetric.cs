using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Domain.Metrics.Formatters;

namespace DiskAnalyzer.Library.Domain.Metrics.Files;

[MetricName("Общее количество файлов")]
public class FileCountMetric : BaseMetric, IFileMetric
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