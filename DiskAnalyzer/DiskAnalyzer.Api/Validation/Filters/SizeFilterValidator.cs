using DiskAnalyzer.Infrastructure.Filters;

namespace DiskAnalyzer.Api.Validation.Filters
{
    public class SizeFilterValidator : IFilterValidator
    {
        public void Validate(object value)
        {
            var filter = value as SizeFilter;

            if (filter == null)
            {
                return;
            }

            if (filter.MinSizeBytes < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(filter.MinSizeBytes),
                    "The minimum size must be non-negative");

            if (filter.MaxSizeBytes < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(filter.MaxSizeBytes),
                    "The maximum size must be non-negative");

            if (filter.MinSizeBytes > filter.MaxSizeBytes)
                throw new ArgumentOutOfRangeException(
                    nameof(filter.MaxSizeBytes),
                    "The maximum size must be greater than the minimum");
        }
    }
}
