using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers
{
    public record GroupingMeasurementDto(
        string Path,
        [param: Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
        IEnumerable<FilesMeasurementType> MeasurementTypes,
        FilesGroupingType GroupingType,
        IEnumerable<FilterDto>? Filters,
        bool SaveToHistory = false);

    [ApiController]
    [Route("api/measurements/groups")]
    public class GroupingMeasurementsController : AnalysisControllerBase
    {
        private readonly IFilesGrouper filesGrouper;
        private readonly IRepository repository;

        public GroupingMeasurementsController(IFilesGrouper filesGrouper, IRepository repository)
        {
            this.filesGrouper = filesGrouper;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Make(GroupingMeasurementDto dto)
        {
            try
            {
                var filter = FilterFactory.Create(dto.Filters);
                var measurment = FilesMesurementFactory.Create(dto.MeasurementTypes);
                var grouper = GrouperFactory.Create(dto.GroupingType);
                var result = filesGrouper.GroupFiles(dto.Path, dto.MaxDepth, measurment, grouper, filter);

                if (dto.SaveToHistory)
                {
                    await repository.AddAsync(result);
                }

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
