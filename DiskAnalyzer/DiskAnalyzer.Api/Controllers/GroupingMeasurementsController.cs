using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;
using DiskAnalyzer.Library.Domain.Measurements.GroupsInDirectory;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.Library.Infrastructure.Groupers;
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
        private static List<GroupingRecord>? lastResult;

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

            lastResult = new();
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

            foreach (var gr in new GroupMeasurement().MeasureGroupsInDirectory(dto.Path, dto.MaxDepth, grouper, compositeFilter))
            {
                lastResult.Add(gr);
            }

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
