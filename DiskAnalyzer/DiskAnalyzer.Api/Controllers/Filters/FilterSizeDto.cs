namespace DiskAnalyzer.Api.Controllers.Filters
{
    public record FilterSizeDto(long Min, long Max) : FilterDto
    {
        public override FilterType Type => FilterType.Size;
    }
}
