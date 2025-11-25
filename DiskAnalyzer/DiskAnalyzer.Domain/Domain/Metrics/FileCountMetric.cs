namespace DiskAnalyzer.Library.Domain.Metrics;

public class FileCountMetric : IMetric
{
    public string Name => "FileCount";
    public string Value => FormatValue(_value);

    private readonly object _value;

    public FileCountMetric(object value) => _value = value;

    private string FormatValue(object value)
    {
        if (value is int fileCount)
        {
            return $"{fileCount.ToString()} files";
        }
        return value?.ToString() ?? "0 files";
    }
}
