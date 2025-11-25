namespace DiskAnalyzer.Library.Domain.Filters;

public class AccessTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате открытия";

    private readonly DateTime minDate;
    private readonly DateTime maxDate;

    public AccessTimeFilter(DateTime minDate, DateTime maxDate)
    {
        ValidateSize(minDate, maxDate);
        this.minDate = minDate;
        this.maxDate = maxDate;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastAccessTimeUtc <= maxDate && file.LastAccessTimeUtc >= minDate;

    private static void ValidateSize(DateTime minDate, DateTime maxDate)
    {
        if (minDate > maxDate)
            throw new ArgumentOutOfRangeException(
                nameof(maxDate),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
