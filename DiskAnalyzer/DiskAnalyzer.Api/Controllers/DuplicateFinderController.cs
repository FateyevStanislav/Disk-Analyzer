using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers
{
    public record DuplicateFinderDto(
        string Path,
        [param: Range(0, int.MaxValue, ErrorMessage = "Max depth cannot be less than 0")] int MaxDepth,
        IEnumerable<FilterDto>? Filters);

    [ApiController]
    [Route("api/measurements/duplicates")]
    public class DuplicateFinderController : AnalysisControllerBase
    {
        private static DuplicatesFinder duplicatesFinder =
            new DuplicatesFinder(
                new DirectoryWalker(
                    new Logger<DirectoryWalker>(
                        new LoggerFactory())));

        [HttpPost]
        public IActionResult Make(DuplicateFinderDto dto)
        {
            try
            {
                var filter = FilterFactory.Create(dto.Filters);
                var result = duplicatesFinder.FindDuplicates(dto.Path, dto.MaxDepth, filter);
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
}
