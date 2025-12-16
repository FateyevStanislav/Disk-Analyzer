using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Modules;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Infrastructure.Filters;

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
            try
            {
                var filterInfo = ApiReflection.GetFilterInfo(dto.Type);

                var paramValues = new object?[filterInfo.parameters.Length];

                for (int i = 0; i < filterInfo.parameters.Length; i++)
                {
                    var param = filterInfo.parameters[i];

                    if (!dto.FilterParams.TryGetValue(param.Name!, out var rawValue))
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

                var newFilter = (IFileFilter)Activator.CreateInstance(filterInfo.filterType, paramValues)!;
                result.Add(newFilter);
            }

            catch
            {
                throw;
            }
        }

        return result;
    }
}
