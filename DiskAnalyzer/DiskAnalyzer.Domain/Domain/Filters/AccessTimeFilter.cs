namespace DiskAnalyzer.Library.Domain.Filters;

public class AccessTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате открытия";

    private readonly DateTime minDateUtc;
    private readonly DateTime maxDateUtc;

    public AccessTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateSize(minDateUtc, maxDateUtc);
        this.minDateUtc = minDateUtc;
        this.maxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastAccessTimeUtc <= maxDateUtc && file.LastAccessTimeUtc >= minDateUtc;

    private static void ValidateSize(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
