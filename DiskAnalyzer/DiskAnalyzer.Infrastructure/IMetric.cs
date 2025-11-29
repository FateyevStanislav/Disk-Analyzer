namespace DiskAnalyzer.Infrastructure;

public interface IMetric
{
    string Name { get; }
    string Value { get; }
}