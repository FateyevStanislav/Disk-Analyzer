using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers
{
    public record GroupingMeasurementDto(
        string Path,
        [param: Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
        IEnumerable<FilesMeasurementType> MeasurementTypes,
        FilesGroupingType GroupingType,
        IEnumerable<FilterDto>? Filters);

    [ApiController]
    [Route("api/measurements/groups")]
    public class GroupingMeasurementsController : AnalysisControllerBase
    {
        private static FilesGrouper filesGrouper =
            new FilesGrouper(
                new DirectoryWalker(
                    new Logger<DirectoryWalker>(
                        new LoggerFactory())));

        [HttpPost]
        public IActionResult Make(GroupingMeasurementDto dto)
        {
            try
            {
                var filter = FilterFactory.Create(dto.Filters);
                var measurment = FilesMesurementFactory.Create(dto.MeasurementTypes);
                var grouper = GrouperFactory.Create(dto.GroupingType);
                var result = filesGrouper.GroupFiles(dto.Path, dto.MaxDepth, measurment, grouper, filter);
                return OkAnalysis(result);
            }

            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
