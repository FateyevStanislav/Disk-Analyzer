using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Filters;

[FilterType("CreationTime")]
public class CreationTimeFilter : IFileFilter
{
    [FilterInfo("MinDate")]
    public DateTime MinDateUtc { get; }

    [FilterInfo("MaxDate")]
    public DateTime MaxDateUtc { get; }

    public CreationTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateSize(minDateUtc, maxDateUtc);
        MinDateUtc = minDateUtc;
        MaxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.CreationTimeUtc <= MaxDateUtc && file.CreationTimeUtc >= MinDateUtc;

    private static void ValidateSize(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}