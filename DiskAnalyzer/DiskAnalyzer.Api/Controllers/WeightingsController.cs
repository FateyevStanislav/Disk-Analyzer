using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

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
            var repo = new WeightingRecordRepository();
            var filter = dto.FilterExtension != null ? new ExtensionFilter(dto.FilterExtension.Extension) : null;

            WeightingRecord result;
            switch (dto.Type)
            {
                case WeightingType.Count:
                    result = FileWeigher.CountFiles(dto.Path, dto.MaxDepth, filter);
                    break;

                case WeightingType.Size:
                    result = FileWeigher.CalcTotalSize(dto.Path, dto.MaxDepth, filter);
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
                var repo = new WeightingRecordRepository();
                return Ok(repo.Get(id));
            }

            catch
            {
                return NotFound();
            }
        }
    }
}
