using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Filters;

[FilterType("Size")]
public class SizeFilter : IFileFilter
{
    [FilterInfo("MinSize")]
    public long MinSizeBytes { get; }

    [FilterInfo("MaxSize")]
    public long MaxSizeBytes { get; }

    public SizeFilter(long minSizeBytes, long maxSizeBytes)
    {
        ValidateSize(minSizeBytes, maxSizeBytes);
        MinSizeBytes = minSizeBytes;
        MaxSizeBytes = maxSizeBytes;
    }

    public bool ShouldInclude(FileInfo file)
        => file.Length <= MaxSizeBytes && file.Length >= MinSizeBytes;

    private static void ValidateSize(long minSizeBytes, long maxSizeBytes)
    {
        if (minSizeBytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(minSizeBytes),
                "Минимальный размер должен быть неотрицательным");
        if (maxSizeBytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(maxSizeBytes),
                "Максимальный размер должен быть неотрицательным");
        if (minSizeBytes > maxSizeBytes)
            throw new ArgumentOutOfRangeException(
                nameof(maxSizeBytes),
                "Максимальный размер должен быть больше минимального");
    }
}