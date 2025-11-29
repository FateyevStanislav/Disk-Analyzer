using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Domain.Measurements.FilesInDirectory;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

public enum FilesMeasurementType
{
    Count,
    Size
}

public record RequestDto(FilesMeasurementType Type, string Path, int MaxDepth, IEnumerable<FilterDto>? Filters);

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : ControllerBase
{
    private static DirectoryMeasurementRecord? lastResult;

    [HttpPost]
    public IActionResult Create(RequestDto dto)
    {
        var filters = dto.Filters?
            .Select(FilterFactory.Create)
            .ToList() ?? new List<IFileFilter>();
        var compositeFilter = new CompositeFilter();
        foreach (var filter in filters)
        {
            compositeFilter.Add(filter);
        }

        switch (dto.Type)
        {
            case FilesMeasurementType.Count:
                lastResult = new FilesCountMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, compositeFilter);
                break;

            case FilesMeasurementType.Size:
                lastResult = new FilesSizeMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, compositeFilter);
                break;

            default:
                return BadRequest("Uncorrect weighting type");
        }

        return Ok(lastResult);
    }

    [HttpPost("saveToHistory")]
    public IActionResult Save()
    {
        if (lastResult == null)
        {
            return BadRequest("Measurement is missing or has already been added to history");
        }

        HistoryController.AddIdToHistory(lastResult);
        lastResult = null;
        return Ok();
    }
}
