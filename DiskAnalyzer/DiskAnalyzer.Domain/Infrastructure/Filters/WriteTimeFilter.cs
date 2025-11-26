using DiskAnalyzer.Library.Domain.Attributes;

namespace DiskAnalyzer.Library.Infrastructure.Filters;

[FilterName("Выбор по дате изменения")]
public class WriteTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате изменения";

    private readonly DateTime minDateUtc;
    private readonly DateTime maxDateUtc;

    public WriteTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateSize(minDateUtc, maxDateUtc);
        this.minDateUtc = minDateUtc;
        this.maxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastWriteTimeUtc <= maxDateUtc && file.LastWriteTimeUtc >= minDateUtc;

    private static void ValidateSize(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}