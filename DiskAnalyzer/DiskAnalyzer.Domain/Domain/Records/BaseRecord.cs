using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain.Records;

public abstract class BaseRecord : Entity<Guid>, IRecord
{
    protected BaseRecord(Guid id) : base(id) => CreatedAt = DateTime.UtcNow;

    public abstract string Path { get; init; }
    public DateTime CreatedAt { get; init; }
    public abstract IReadOnlyCollection<string> Logs { get; init; }
    public abstract IReadOnlyCollection<IMetric> Metrics { get; init; }
}