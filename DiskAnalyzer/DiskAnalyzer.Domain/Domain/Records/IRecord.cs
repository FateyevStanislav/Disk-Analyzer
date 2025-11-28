using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.Library.Domain.Records;

public interface IRecord
{
    string Path { get; init; }
    DateTime CreatedAt { get; init; }
    IReadOnlyCollection<string> Logs { get; init; }
    IReadOnlyCollection<IMetric> Metrics { get; init; }
}