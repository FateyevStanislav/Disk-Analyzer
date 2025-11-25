using DiskAnalyzer.Library.Domain.Attributes;

namespace DiskAnalyzer.Library.Domain.Filters;

[FilterName("Выбор по дате изменения")]
public class WriteTimeFilter : IFileFilter
{
    public string Name => "Выбор по дате изменения";

    private readonly DateTime minDate;
    private readonly DateTime maxDate;

    public WriteTimeFilter(DateTime minDate, DateTime maxDate)
    {
        ValidateSize(minDate, maxDate);
        this.minDate = minDate;
        this.maxDate = maxDate;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastWriteTime <= maxDate && file.LastWriteTime >= minDate;

    private static void ValidateSize(DateTime minDate, DateTime maxDate)
    {
        if (minDate > maxDate)
            throw new ArgumentOutOfRangeException(
                nameof(maxDate),
                "Максимальная дата должна быть не меньше минимальной");
    }
}
