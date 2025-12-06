using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using DiskAnalyzer.Infrastructure.Filter;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

public record FilesMeasurementDto(FilesMeasurementStrategyType strategyType, string Path, int MaxDepth, IEnumerable<FilterDto>? Filters);

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : ControllerBase
{
    private static Record? lastResult;
    private static FilesMeasurer filesMeasurer =
        new FilesMeasurer(
            new DirectoryWalker(
                new Logger<DirectoryWalker>(
                    new LoggerFactory())));

    [HttpPost]
    public IActionResult Create(FilesMeasurementDto dto)
    {

        var filter = FilterFactory.Create(dto.Filters);

        IFilesMeasurementStrategy strategy;
        try
        {
            strategy = FilesMesurementStrategyFactory.Create(dto.strategyType);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        lastResult = filesMeasurer.MeasureFiles(dto.Path, dto.MaxDepth, strategy, filter);

        return Ok(lastResult);
    }

    [HttpPost("saveToHistory")]
    public IActionResult Save()
    {
        if (lastResult == null)
        {
            return BadRequest("Measurement is missing or has already been added to history");
        }

        HistoryController.history.AddRecord(lastResult);
        lastResult = null;
        return Ok();
    }
}
