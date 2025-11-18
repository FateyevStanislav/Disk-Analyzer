using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain
{
    public class FileCountMetric : Metric<int>
    {
        public override string Name => "File Count";
        public FileCountMetric(int count) : base(count) { }
    }
}
