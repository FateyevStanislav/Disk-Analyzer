using DiskAnalyzer.Infrastructure.Filters;

namespace DiskAnalyzer.Api.Validation.Filters;

public class TimeValidator : IFilterValidator
{
    public void Validate(object value)
    {
        try
        {
            var dt = GetDatetime(value);
            if (dt.Item1 > dt.Item2)
                throw new ArgumentOutOfRangeException(
                    "The maximum date must be no less than the minimum date");
        }
        catch
        {
            throw;
        }
    }

    private (DateTime, DateTime) GetDatetime(object value)
    {
        var filterAt = value as AccessTimeFilter;
        if (filterAt != null)
        {
            return (filterAt.MinDateUtc, filterAt.MaxDateUtc);
        }

        var filterCt = value as CreationTimeFilter;
        if (filterCt != null)
        {
            return (filterCt.MinDateUtc, filterCt.MaxDateUtc);
        }

        var filterWt = value as WriteTimeFilter;
        if (filterWt != null)
        {
            return (filterWt.MinDateUtc, filterWt.MaxDateUtc);
        }

        throw new ArgumentException("Unknown time filter type");
    }
}
