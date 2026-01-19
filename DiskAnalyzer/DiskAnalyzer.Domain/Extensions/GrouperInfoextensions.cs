using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;
using DiskAnalyzer.Domain.Models;
using System.Collections.Concurrent;
using System.Reflection;

namespace DiskAnalyzer.Domain.Extensions;

internal static class GrouperInfoExtensions
{
    private static readonly ConcurrentDictionary<Type, string> cache = new();

    public static GrouperInfo ToGrouperInfo(this IFileGrouper grouper)
    {
        var type = grouper.GetType();

        var typeName = cache.GetOrAdd(type, t =>
        {
            var attr = t.GetCustomAttribute<GrouperTypeAttribute>();
            return attr?.DisplayName ?? t.Name.Replace("Grouper", "");
        });

        return new GrouperInfo(typeName);
    }
}