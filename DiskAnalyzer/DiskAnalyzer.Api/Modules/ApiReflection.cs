using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Infrastructure.Filters;
using System.Reflection;

namespace DiskAnalyzer.Api.Modules;

public record FilterFactoryInfo(Type filterType, ParameterInfo[] parameters);

public static class ApiReflection
{
    private static Dictionary<string, FilterFactoryInfo> filtersInfo = new();

    public static void InitData()
    {
        var domainAssembly = typeof(ExtensionFilter).Assembly;

        var filterTypes = domainAssembly.GetTypes().
            Where(t => t.GetInterfaces().Contains(typeof(IFileFilter)));

        foreach (var f in filterTypes)
        {
            if (f == typeof(CompositeFilter))
            {
                continue;
            }

            var constructor = f.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                throw new Exception($"No constructor in filter {f.Name}");
            }
            var constructorParams = constructor.GetParameters();

            filtersInfo.Add(f.Name, new FilterFactoryInfo(f, constructorParams));
        }
    }

    public static IReadOnlyDictionary<string, Dictionary<string, string>> GetFiltersData()
    {
        return filtersInfo.ToDictionary(
            f => f.Key,
            f => f.Value.parameters.ToDictionary(
                p => p.Name!,
                p => p.ParameterType.FullName
            )
        ).AsReadOnly()!;
    }

    public static FilterFactoryInfo GetFilterInfo(string filterName)
    {
        if (!filtersInfo.ContainsKey(filterName))
        {
            throw new ArgumentException($"Unknown filter type: {filterName}");
        }

        return filtersInfo[filterName];
    }
}
