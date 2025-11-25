using DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public enum FilterType
    {
        Extension,
        Size
    }

    public record FilterExtensionDto(string Extension);


    public enum WeightingType
    {
        Count,
        Size
    }

    public record RequestDto(WeightingType Type, string Path, int MaxDepth, FilterExtensionDto? FilterExtension, bool SaveInHistory);


    [ApiController]
    [Route("api/[controller]")]
    public class WeightingsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(RequestDto dto)
        {
            var repo = new ConcDictRepository();
            var filter = dto.FilterExtension != null ? new ExtensionFilter(dto.FilterExtension.Extension) : null;

            DirectoryMeasurmentRecord result;
            switch (dto.Type)
            {
                case WeightingType.Count:
                    result = new FilesCountMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, filter);
                    break;

                case WeightingType.Size:
                    result = new FilesSizeMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, filter);
                    break;

                default:
                    return BadRequest("Uncorrect weighting type");
            }

            if (dto.SaveInHistory)
            {
                HistoryController.AddIdToHistory(result.Id);
            }

            repo.Add(result);

            return Created($"/api/Weightings/{result.Id}", new { result.Id });
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var repo = new ConcDictRepository();
                return Ok(repo.Get(id));
            }

            catch
            {
                return NotFound();
            }
        }
    }
}
