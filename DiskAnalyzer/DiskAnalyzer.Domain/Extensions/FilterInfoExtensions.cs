using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure.Filter;
using System.Collections.Concurrent;
using System.Reflection;

namespace DiskAnalyzer.Domain.Extensions;

public static class FilterInfoExtensions
{
    private static readonly ConcurrentDictionary<Type, (string typeName, PropertyInfo[] props)> cache
        = new();

    public static FilterInfo ToFilterInfo(this IFileFilter filter)
    {
        var type = filter.GetType();

        var cached = cache.GetOrAdd(type, t =>
        {
            var filterType = t.GetCustomAttribute<FilterTypeAttribute>()
                ?? new FilterTypeAttribute(t.Name.Replace("Filter", ""));

            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead &&
                           p.GetIndexParameters().Length == 0 &&
                           p.GetCustomAttribute<FilterInfoAttribute>() != null)
                .ToArray();

            return (filterType.DisplayName, props);
        });

        var parameters = cached.props.ToDictionary(
            p => p.GetCustomAttribute<FilterInfoAttribute>()!.Name,
            p => ConvertToString(p.GetValue(filter)));

        return new FilterInfo(cached.typeName, parameters);
    }

    public static IReadOnlyCollection<FilterInfo>? ToFilterInfoList(this IFileFilter? filter)
    {
        if (filter == null) return null;

        return filter switch
        {
            CompositeFilter composite => composite.Filters.Select(ToFilterInfo).ToList(),
            _ => new[] { filter.ToFilterInfo() }
        };
    }

    private static string ConvertToString(object? value)
    {
        if (value == null) return "null";

        if (value is DateTime dateTime)
            return dateTime.ToString("o"); 

        if (value is bool boolean)
            return boolean.ToString().ToLowerInvariant(); 

        return value.ToString() ?? "null";
    }
}
