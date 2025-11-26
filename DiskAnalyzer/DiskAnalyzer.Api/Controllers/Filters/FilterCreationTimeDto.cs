namespace DiskAnalyzer.Api.Controllers.Filters
{
    public record FilterCreationTimeDto(DateTime MinDateUtc, DateTime MaxDateUtc) : FilterDto
    {
        public override FilterType Type => FilterType.CreationTime;
    }
}
