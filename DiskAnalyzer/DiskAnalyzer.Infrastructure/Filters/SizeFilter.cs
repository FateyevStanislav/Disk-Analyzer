using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Infrastructure.Filters;

[FilterType("Size")]
public class SizeFilter : IFileFilter
{
    [FilterInfo("MinSize")]
    public long MinSizeBytes { get; }

    [FilterInfo("MaxSize")]
    public long MaxSizeBytes { get; }

    public SizeFilter(long minSizeBytes, long maxSizeBytes)
    {
        MinSizeBytes = minSizeBytes;
        MaxSizeBytes = maxSizeBytes;
    }

    public bool ShouldInclude(FileInfo file)
        => file.Length <= MaxSizeBytes && file.Length >= MinSizeBytes;
}