using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Validation;

internal sealed class ExistingPathAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object?  value, ValidationContext context)
    {
        var path = value as string;

        if(path == null || string.IsNullOrWhiteSpace(path))
        {
            return new ValidationResult("Path can't be empty");
        }

        if (!Directory.Exists(path))
        {
            return new ValidationResult($"No directory: {path}");
        }

        return ValidationResult.Success;
    }
}
