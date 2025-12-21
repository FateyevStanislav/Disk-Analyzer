using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers;

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : AnalysisControllerBase
{
    private readonly IFilesMeasurer filesMeasurer;
    private readonly IRepository repository;

    public FilesMeasurementsController(IFilesMeasurer filesMeasurer, IRepository repository)
    {
        this.filesMeasurer = filesMeasurer;
        this.repository = repository;
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
