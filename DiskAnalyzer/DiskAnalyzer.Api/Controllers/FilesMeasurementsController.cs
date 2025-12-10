using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

public record FilesMeasurementDto(IEnumerable<FilesMeasurementType> MeasurementTypes, string Path, int MaxDepth, IEnumerable<FilterDto>? Filters);

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : ControllerBase
{
    private static FilesMeasurer filesMeasurer =
        new FilesMeasurer(
            new DirectoryWalker(
                new Logger<DirectoryWalker>(
                    new LoggerFactory())));

    [HttpPost]
    public IActionResult Create(FilesMeasurementDto dto)
    {

        var filter = FilterFactory.Create(dto.Filters);
        var measurment = FilesMesurementFactory.Create(dto.MeasurementTypes);
        return Ok(filesMeasurer.MeasureFiles(dto.Path, dto.MaxDepth, measurment, filter));
    }
}
