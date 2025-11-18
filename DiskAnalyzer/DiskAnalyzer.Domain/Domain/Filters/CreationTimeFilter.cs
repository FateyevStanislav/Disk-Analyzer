namespace DiskAnalyzer.Library.Domain.Filters;

public class CreationTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате создания";

    private readonly DateTime minDate;
    private readonly DateTime maxDate;

    public CreationTimeFilter(DateTime minDate, DateTime maxDate)
    {
        ValidateSize(minDate, maxDate);
        this.minDate = minDate;
        this.maxDate = maxDate;
    }

    public bool ShouldInclude(FileInfo file)
        => file.CreationTime <= maxDate && file.CreationTime >= minDate;

    private static void ValidateSize(DateTime minDate, DateTime maxDate)
    {
        if (minDate > maxDate)
            throw new ArgumentOutOfRangeException(
                nameof(maxDate),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
