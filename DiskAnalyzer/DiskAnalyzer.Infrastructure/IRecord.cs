namespace DiskAnalyzer.Infrastructure;

public interface IRecord
{
    string Path { get; init; }
    DateTime CreatedAt { get; init; }
    IReadOnlyCollection<IMetric> Metrics { get; init; }
}