using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions;
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
    IEnumerable<FilterDto>? Filters,
    bool SaveToHistory = false);

[ApiController]
[Route("api/measurements/files")]
public class FilesMeasurementsController : AnalysisControllerBase
{
    private readonly FilesMeasurer filesMeasurer;
    private readonly IRepository repository;

    public FilesMeasurementsController(FilesMeasurer filesMeasurer, IRepository repository)
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
