using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain.Metrics
{
    public class FileSizeMetric : IMetric
    {
        public string Name => "FileSize";
        public string Value => FormatValue(_value);

        private readonly object _value;

        public FileSizeMetric(object value) => _value = value;

        private string FormatValue(object value)
        {
            if (value is long sizeInBytes)
            {
                return SizeFormatter.FormatBytes(sizeInBytes);
            }
            return value?.ToString() ?? "0 bytes";
        }
    }
}