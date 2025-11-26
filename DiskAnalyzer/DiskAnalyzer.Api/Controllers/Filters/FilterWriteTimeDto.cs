namespace DiskAnalyzer.Api.Controllers.Filters
{
    public record FilterWriteTimeDto(DateTime MinDateUtc, DateTime MaxDateUtc) : FilterDto
    {
        public override FilterType Type => FilterType.CreationTime;
    }
}
