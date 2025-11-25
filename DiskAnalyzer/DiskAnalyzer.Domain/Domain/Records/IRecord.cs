using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.Library.Domain.Records;

public interface IRecord
{
    public string Path { get; init; }
    public DateTime CreatedAt { get; init; }
    public IReadOnlyCollection<string> Logs { get; init; }
    public IReadOnlyCollection<IMetric> Metrics { get; init; }
}
