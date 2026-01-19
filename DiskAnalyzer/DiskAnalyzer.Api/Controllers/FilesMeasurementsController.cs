using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : AnalysisControllerBase
{
    private readonly IFilesMeasurer filesMeasurer;

    public FilesMeasurementsController(IFilesMeasurer filesMeasurer)
    {
        this.filesMeasurer = filesMeasurer;
    }

    [HttpPost]
    public async Task<IActionResult> Make(FilesMeasurementDto dto)
    {
        try
        {
            var filter = FilterFactory.Create(dto.Filters);
            var measurment = FilesMesurementFactory.Create(dto.MeasurementTypes);
            var result = filesMeasurer.MeasureFiles(dto.Path, dto.MaxDepth, measurment, filter);
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
