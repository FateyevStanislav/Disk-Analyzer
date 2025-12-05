using DiskAnalyzer.Infrastructure.Filter;

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
        ValidateDateSpan(minDateUtc, maxDateUtc);
        MinDateUtc = minDateUtc;
        MaxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.CreationTimeUtc <= MaxDateUtc && file.CreationTimeUtc >= MinDateUtc;

    private static void ValidateDateSpan(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}