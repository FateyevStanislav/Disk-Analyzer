using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;
using System.Reflection;

public static class FilterInfoExtensions
{
    public static FilterInfo ToFilterInfo(this IFileFilter filter)
    {
        var filterType = filter.GetType().GetCustomAttribute<FilterTypeAttribute>()
            ?? new FilterTypeAttribute(filter.GetType().Name.Replace("Filter", ""));

        var parameters = filter.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
            .Where(p => p.GetCustomAttribute<FilterInfoAttribute>() != null)
            .ToDictionary(
                p => p.GetCustomAttribute<FilterInfoAttribute>()!.Name,
                p => p.GetValue(filter) ?? "null");

        return new FilterInfo(filterType.DisplayName, parameters);
    }

    public static IReadOnlyCollection<FilterInfo>? ToFilterInfoList(this IFileFilter? filter)
    {
        if (filter == null) return null;
        return filter switch
        {
            CompositeFilter composite => [.. composite.Filters.Select(ToFilterInfo)],
            _ => new[] { filter.ToFilterInfo() }
        };
    }
}