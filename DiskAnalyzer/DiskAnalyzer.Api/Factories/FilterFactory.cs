using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Modules;
using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure.Filter;
using System.Reflection.Metadata;

public static class FilterFactory
{
    public static IFileFilter? Create(IEnumerable<FilterDto>? filters)
    {
        if (filters == null || filters.Count() == 0)
        {
            return null;
        }

        var result = new CompositeFilter();

        foreach (var dto in filters)
        {
            var filterType = ApiReflection.GetFilterType(dto.Type);
            if (filterType == null)
            {
                throw new ArgumentException($"Unknown filter type: {dto.Type}");
            }

            var constructor = filterType.GetConstructors().FirstOrDefault();
            if (constructor == null)
            {
                throw new ArgumentException($"Filter {dto.Type} has no constructor");
            }

            var constructorParams = constructor.GetParameters();
            var paramValues = new object?[constructorParams.Length];

            for (int i = 0; i < constructorParams.Length; i++)
            {
                var param = constructorParams[i];

                if(!dto.FilterParams.TryGetValue(param.Name!, out var rawValue))
                {
                    throw new ArgumentException($"Missing requited parameter {param.Name}");
                }

                try
                {
                    paramValues[i] = Convert.ChangeType(rawValue, param.ParameterType);
                }

                catch
                {
                    throw new ArgumentException($"Invalid value \"{rawValue}\" for parameter {param.Name}");
                }
            }

            var newFilter = (IFileFilter)Activator.CreateInstance(filterType, paramValues)!;
            result.Add(newFilter);
        }

        return result;
    }
}
