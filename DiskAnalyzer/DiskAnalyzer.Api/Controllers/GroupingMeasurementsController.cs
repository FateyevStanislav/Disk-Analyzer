using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public record GroupingMeasurementDto(
        string Path,
        int MaxDepth,
        IEnumerable<FilesMeasurementType> MeasurementTypes,
        FilesGroupingType GroupingType,
        IEnumerable<FilterDto>? Filters);

    [ApiController]
    [Route("api/measurements/groups")]
    public class GroupingMeasurementsController : ControllerBase
    {
        private static FilesGrouper filesGrouper =
            new FilesGrouper(
                new DirectoryWalker(
                    new Logger<DirectoryWalker>(
                        new LoggerFactory())));

        [HttpPost]
        public IActionResult Create(GroupingMeasurementDto dto)
        {
            var filter = FilterFactory.Create(dto.Filters);
            var measurment = FilesMesurementFactory.Create(dto.MeasurementTypes);
            var grouper = GrouperFactory.Create(dto.GroupingType);

            return Ok(filesGrouper.GroupFiles(dto.Path, dto.MaxDepth, measurment, grouper, filter));
        }
    }
}
