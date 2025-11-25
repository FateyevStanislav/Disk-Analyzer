namespace DiskAnalyzer.Library.Domain.Metrics.Formatters;

public class CountFormatter : IValueFormatter
{
    public string Format(object value)
    {
        if (value is int count) return $"{count} files";
        return "0 files";
    }
}
