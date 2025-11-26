using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public enum FilesMeasurementType
    {
        Count,
        Size
    }

    public record RequestDto(FilesMeasurementType Type, string Path, int MaxDepth, IEnumerable<FilterDto>? Filters, bool SaveInHistory);


    [ApiController]
    [Route("api/measurements/files")]
    public class FilesMeasurementsController : ControllerBase
    {
        [HttpPost()]
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

            DirectoryMeasurementRecord result;
            switch (dto.Type)
            {
                case FilesMeasurementType.Count:
                    result = new FilesCountMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, compositeFilter);
                    break;

                case FilesMeasurementType.Size:
                    result = new FilesSizeMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, compositeFilter);
                    break;

                default:
                    return BadRequest("Uncorrect weighting type");
            }

            if (dto.SaveInHistory)
            {
                HistoryController.AddIdToHistory(result);
            }

            return Ok(result);
        }
    }
}
