namespace DiskAnalyzer.Library.Domain.Filters;

public class CreationTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате создания";

    private readonly DateTime minDateUtc;
    private readonly DateTime maxDateUtc;

    public CreationTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        ValidateSize(minDateUtc, maxDateUtc);
        this.minDateUtc = minDateUtc;
        this.maxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.CreationTimeUtc <= maxDateUtc && file.CreationTimeUtc >= minDateUtc;

    private static void ValidateSize(DateTime minDateUtc, DateTime maxDateUtc)
    {
        if (minDateUtc > maxDateUtc)
            throw new ArgumentOutOfRangeException(
                nameof(maxDateUtc),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
