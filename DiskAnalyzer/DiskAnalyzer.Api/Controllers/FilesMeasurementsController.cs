using DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public record FilterExtensionDto(string Extension);

    public enum FilesMeasurementType
    {
        Count,
        Size
    }

    public record RequestDto(FilesMeasurementType Type, string Path, int MaxDepth, FilterExtensionDto? FilterExtension, bool SaveInHistory);


    [ApiController]
    [Route("api/measurements/files")]
    public class FilesMeasurementsController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Create(RequestDto dto)
        {
            var filter = dto.FilterExtension != null ? new ExtensionFilter(dto.FilterExtension.Extension) : null;

            DirectoryMeasurementRecord result;
            switch (dto.Type)
            {
                case FilesMeasurementType.Count:
                    result = new FilesCountMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, filter);
                    break;

                case FilesMeasurementType.Size:
                    result = new FilesSizeMeasurement().MeasureFilesInDirectory(dto.Path, dto.MaxDepth, filter);
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
