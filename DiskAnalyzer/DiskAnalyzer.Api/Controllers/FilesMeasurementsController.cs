using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DiskAnalyzer.Api.Controllers;

public record FilesMeasurementDto(
    string Path,
    [param: Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
    IEnumerable<FilesMeasurementType> MeasurementTypes,
    IEnumerable<FilterDto>? Filters);

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : AnalysisControllerBase
{
    private static FilesMeasurer filesMeasurer =
        new FilesMeasurer(
            new DirectoryWalker(
                new Logger<DirectoryWalker>(
                    new LoggerFactory())));

    [HttpPost]
    public IActionResult Make(FilesMeasurementDto dto)
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
