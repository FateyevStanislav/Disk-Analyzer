using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain
{
    public class FileSizeMetric : Metric<long>
    {
        public override string Name => "Total Size";
        public FileSizeMetric(long size) : base(size) { }
    }
}
