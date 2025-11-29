namespace DiskAnalyzer.Domain.Metrics.Formatters;

public interface IValueFormatter
{
    string Format(object value);
}