using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Models.Filters;
using System.Reflection;

namespace DiskAnalyzer.Api.Modules;

internal record FilterFactoryInfo(Type filterType, ParameterInfo[] parameters);

internal static class ApiReflection
{
    private static Dictionary<string, FilterFactoryInfo> filtersInfo = new();

    public static void InitData()
    {
        var infrastructureAssembly = typeof(ExtensionFilter).Assembly;

        var filterTypes = infrastructureAssembly.GetTypes()
            .Where(t =>
                t.IsClass 
                && !t.IsAbstract 
                && t.GetInterfaces().Contains(typeof(IFileFilter)) 
                && t != typeof(CompositeFilter)              
            );

        foreach (var filterType in filterTypes)
        {
            var constructor = filterType.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                throw new Exception($"No public constructor in filter {filterType.Name}");
            }

            var constructorParams = constructor.GetParameters();
            filtersInfo.Add(filterType.Name, new FilterFactoryInfo(filterType, constructorParams));
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
