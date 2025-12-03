using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Domain.Groupers;
using DiskAnalyzer.Domain.Records.RecordStrategies.Grouping;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;
using DiskAnalyzer.Infrastructure.Grouper;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public enum FilesGroupingType
    {
        Extension,
        LastAcessTime,
        SizeBucket
    }

    public record GroupingMeasurementDto(FilesGroupingType Type, string Path, int MaxDepth, IEnumerable<FilterDto>? Filters);

    [ApiController]
    [Route("api/measurements/groups")]
    public class GroupingMeasurementsController : ControllerBase
    {
        private static Record? lastResult;
        private static FilesGrouper filesGrouper =
            new FilesGrouper(
                new DirectoryWalker(
                    new Logger<DirectoryWalker>(
                        new LoggerFactory())));

        [HttpPost]
        public IActionResult Create(GroupingMeasurementDto dto)
        {
            var filters = dto.Filters?
                .Select(FilterFactory.Create)
                .ToList() ?? new List<IFileFilter>();
            var compositeFilter = new CompositeFilter();
            foreach (var filter in filters)
            {
                compositeFilter.Add(filter);
            }

            IFileGrouper grouper;

            switch (dto.Type)
            {
                case FilesGroupingType.Extension:
                    grouper = new ExtensionGrouper();
                    break;

                case FilesGroupingType.LastAcessTime:
                    grouper = new LastAccessTimeGrouper();
                    break;

                case FilesGroupingType.SizeBucket:
                    grouper = new SizeBucketGrouper();
                    break;

                default:
                    return BadRequest("Uncorrect grouper type");
            }

            lastResult = filesGrouper.GroupFiles(dto.Path, dto.MaxDepth, new SizeInfoGroupStrategy(), grouper, compositeFilter);

            return Ok(lastResult);
        }

        //[HttpPost("saveToHistory")]
        //public IActionResult Save()
        //{
        //    if (lastResult == null)
        //    {
        //        return BadRequest("Measurement is missing or has already been added to history");
        //    }

        //    HistoryController.AddIdToHistory(lastResult);
        //    lastResult = null;
        //    return Ok();
        //}
    }
}
