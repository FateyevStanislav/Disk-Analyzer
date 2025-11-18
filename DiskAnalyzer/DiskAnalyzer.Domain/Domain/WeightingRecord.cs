using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain
{
    public class WeightingRecord : Entity<Guid>
    {
        public string Path { get; init; }
        public DateTime CreatedAt { get; init; }
        public IReadOnlyCollection<string> Errors { get; init; }
        public IReadOnlyCollection<Metric> Metrics { get; init; }

        public WeightingRecord(Guid id, string path,
            IReadOnlyCollection<string>? errors,
            IReadOnlyCollection<Metric> metrics) : base(id)
        {
            Path = path;
            Errors = errors ?? Array.Empty<string>();
            Metrics = metrics ?? Array.Empty<Metric>();
            CreatedAt = DateTime.Now;
        }
    }
}
