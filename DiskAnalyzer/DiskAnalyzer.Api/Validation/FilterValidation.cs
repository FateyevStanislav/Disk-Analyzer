using DiskAnalyzer.Api.Validation.Filters;

namespace DiskAnalyzer.Api.Validation;

internal static class FilterValidation
{
    private static Dictionary<Type, IFilterValidator> validators = new();

    public static void RegisterValidator(Type filterType, IFilterValidator validator)
    {
        validators[filterType] = validator;
    }

    public static void ValidateFilter(object filter, Type filterType)
    {
        try
        {
            if (validators.ContainsKey(filterType))
            {
                validators[filterType].Validate(filter);
            }
        }

        catch
        {
            throw;
        }
    }
}
