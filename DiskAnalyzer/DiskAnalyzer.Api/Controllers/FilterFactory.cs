using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Library.Infrastructure.Filters;

public static class FilterFactory
{
    public static IFileFilter Create(FilterDto dto) =>
        dto switch
        {
            FilterAcessTimeDto aTimeDto => new AccessTimeFilter(aTimeDto.MinDateUtc, aTimeDto.MaxDateUtc),
            FilterCreationTimeDto cTimeDto => new CreationTimeFilter(cTimeDto.MinDateUtc, cTimeDto.MaxDateUtc),
            FilterExtensionDto extDto => new ExtensionFilter(extDto.Extension),
            FilterSizeDto sizeDto => new SizeFilter(sizeDto.Min, sizeDto.Max),
            FilterWriteTimeDto wTimeDto => new WriteTimeFilter(wTimeDto.MinDateUtc, wTimeDto.MaxDateUtc),
            _ => throw new NotSupportedException($"Filter type {dto.Type} not supported")
        };
}
