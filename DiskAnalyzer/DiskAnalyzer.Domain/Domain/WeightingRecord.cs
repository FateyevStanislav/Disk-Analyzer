using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain;

public class WeightingRecord : Entity<Guid>
{
    public string Path { get; init; }
    public int MaxDepth { get; init; }
    public int FileCount { get; init; }
    public string? Error { get; init; }

    public WeightingRecord(Guid id, string path, int maxDepth, int fileCount, string? error) : base(id)
    {
        Path = path;
        MaxDepth = maxDepth;
        FileCount = fileCount;
        Error = error;
    }
}