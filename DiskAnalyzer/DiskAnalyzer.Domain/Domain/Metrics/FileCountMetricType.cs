namespace DiskAnalyzer.Library.Domain
{
    public class FileCountMetricType : IMetric
    {
        public string Name => "FileCount";
        public string Value => FormatValue(_value);

        private readonly object _value;

        public FileCountMetricType(object value) => _value = value;

        private string FormatValue(object value)
        {
            if (value is int fileCount)
            {
                return $"{fileCount.ToString()} files";
            }
            return value?.ToString() ?? "0 files";
        }
    }
}
