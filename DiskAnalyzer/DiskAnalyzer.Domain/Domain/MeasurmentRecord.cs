using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain;

public class MeasurmentRecord : Entity<Guid>
{
    public string Path { get; init; }
    public DateTime CreatedAt { get; init; }
    public IReadOnlyCollection<string> Logs { get; init; }
    public IReadOnlyCollection<IMetric> Metrics { get; init; }

    public MeasurmentRecord(Guid id, string path,
        IReadOnlyCollection<string>? logs,
        IReadOnlyCollection<IMetric> metrics) : base(id)
    {
        Path = path;
        Logs = logs ?? Array.Empty<string>();
        Metrics = metrics ?? Array.Empty<IMetric>();
        CreatedAt = DateTime.UtcNow;
    }
}
