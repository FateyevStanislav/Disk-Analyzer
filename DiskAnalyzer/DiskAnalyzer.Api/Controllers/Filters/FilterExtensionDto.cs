namespace DiskAnalyzer.Api.Controllers.Filters
{
    public record FilterExtensionDto(string Extension) : FilterDto
    {
        public override FilterType Type => FilterType.Extension;
    }
}
