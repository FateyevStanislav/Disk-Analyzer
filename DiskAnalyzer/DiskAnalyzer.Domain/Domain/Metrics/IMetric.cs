namespace DiskAnalyzer.Library.Domain.Metrics;

public interface IMetric
{
    string Name { get; }
    string Value { get; }
}
