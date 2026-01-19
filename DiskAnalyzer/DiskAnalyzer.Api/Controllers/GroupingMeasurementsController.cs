using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/measurements/groups")]
    public class GroupingMeasurementsController : AnalysisControllerBase
    {
        private readonly IFilesGrouper filesGrouper;

        public GroupingMeasurementsController(IFilesGrouper filesGrouper)
        {
            this.filesGrouper = filesGrouper;
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
