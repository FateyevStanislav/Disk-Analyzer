namespace DiskAnalyzer.Library.Domain.Filters;

public class AccessTimeFilte : IFileFilter
{
    public string Name => "Выбор по дате открытия";

    private readonly DateTime minDate;
    private readonly DateTime maxDate;

    public AccessTimeFilte(DateTime minDate, DateTime maxDate)
    {
        ValidateSize(minDate, maxDate);
        this.minDate = minDate;
        this.maxDate = maxDate;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastAccessTime <= maxDate && file.LastAccessTime >= minDate;

    private static void ValidateSize(DateTime minDate, DateTime maxDate)
    {
        if (minDate > maxDate)
            throw new ArgumentOutOfRangeException(
                nameof(maxDate),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
