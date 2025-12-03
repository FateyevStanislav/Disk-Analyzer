using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure.Filter;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Reflection.Metadata;

namespace DiskAnalyzer.Api.Modules;

public static class ApiReflection
{
    private static Assembly domainAssembly;
    private static Dictionary<string, Dictionary<string, Type>> filtersData;

    public static void InitData()
    {
        domainAssembly = typeof(ExtensionFilter).Assembly;

        filtersData = new();

        var filterTypes = domainAssembly.GetTypes().
            Where(t => t.GetInterfaces().Contains(typeof(IFileFilter)));

        foreach (var f in filterTypes)
        {
            if (f == typeof(CompositeFilter))
            {
                continue;
            }

            filtersData[f.Name] = new();
            foreach (var param in f.GetConstructors()[0].GetParameters())
            {
                filtersData[f.Name].Add(param.Name, param.ParameterType);
            }
        }
    }
    
    public static IReadOnlyDictionary<string, Dictionary<string, Type>> GetFiltersData()
    {

        return filtersData.AsReadOnly();
    }

    public static Type? GetFilterType(string filterName)
    {
        return domainAssembly.GetType($"DiskAnalyzer.Domain.Filters.{filterName}");
    }
}

