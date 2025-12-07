using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Infrastructure.Filters;

[FilterType("AccessTime")]
public class AccessTimeFilter : IFileFilter
{
    [FilterInfo("MinDate")]
    public DateTime MinDateUtc { get; }

    [FilterInfo("MaxDate")]
    public DateTime MaxDateUtc { get; }

    public AccessTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateDateSpan(minDateUtc, maxDateUtc);
        MinDateUtc = minDateUtc;
        MaxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastAccessTimeUtc <= MaxDateUtc && file.LastAccessTimeUtc >= MinDateUtc;

    private static void ValidateDateSpan(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}