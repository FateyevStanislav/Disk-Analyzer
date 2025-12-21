using DiskAnalyzer.Domain.Abstractions;

namespace DiskAnalyzer.Api.Validation.Filters;

internal interface IFilterValidator
{
    public void Validate(object value);
}
