namespace DiskAnalyzer.Library.Infrastructure.Filters;

public class SizeFilter : IFileFilter
{
    public string Name => "Выбор по размеру";

    private readonly long minSizeBytes;
    private readonly long maxSizeBytes;

    public SizeFilter(long minSizeBytes, long maxSizeBytes)
    {
        ValidateSize(minSizeBytes, maxSizeBytes);
        this.minSizeBytes = minSizeBytes;
        this.maxSizeBytes = maxSizeBytes;
    }

    public bool ShouldInclude(FileInfo file)
        => file.Length <= maxSizeBytes && file.Length >= minSizeBytes;

    private static void ValidateSize(long minSizeBytes, long maxSizeBytes)
    {
        if (minSizeBytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(minSizeBytes),
                "Минимальный размер должен быть больше нуля");
        if (maxSizeBytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(maxSizeBytes),
                "Максимальный размер должен быть больше нуля");
        if (minSizeBytes > maxSizeBytes)
            throw new ArgumentOutOfRangeException(
                nameof(maxSizeBytes),
                "Максимальный размер должен быть больше минимального");
    }
}