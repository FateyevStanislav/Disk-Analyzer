namespace DiskAnalyzer.Api.Controllers.Filters
{
    public record FilterAcessTimeDto(DateTime MinDateUtc, DateTime MaxDateUtc) : FilterDto
    {
        public override FilterType Type => FilterType.AcessTime;
    }
}
