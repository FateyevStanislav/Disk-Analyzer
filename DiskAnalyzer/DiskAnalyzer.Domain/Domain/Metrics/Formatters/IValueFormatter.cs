namespace DiskAnalyzer.Library.Domain.Metrics.Formatters;

public interface IValueFormatter
{
    string Format(object value);
}
