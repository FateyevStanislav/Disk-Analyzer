using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Filters;

[FilterType("WriteType")]
public class WriteTimeFilter : IFileFilter
{
    [FilterInfo("MinDate")]
    public DateTime MinDateUtc { get; }

    [FilterInfo("MaxDate")]
    public DateTime MaxDateUtc { get; }

    public WriteTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateSize(minDateUtc, maxDateUtc);
        MinDateUtc = minDateUtc;
        MaxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastWriteTimeUtc <= MaxDateUtc && file.LastWriteTimeUtc >= MinDateUtc;

    private static void ValidateSize(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}