using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    public record DuplicateFinderDto(
        string Path,
        int MaxDepth,
        IEnumerable<FilterDto>? Filters);

    [ApiController]
    [Route("api/measurements/duplicates")]
    public class DuplicateFinderController : ControllerBase
    {
        private static DuplicatesFinder duplicatesFinder =
            new DuplicatesFinder(
                new DirectoryWalker(
                    new Logger<DirectoryWalker>(
                        new LoggerFactory())));

        [HttpPost]
        public IActionResult Create(DuplicateFinderDto dto)
        {
            var filter = FilterFactory.Create(dto.Filters);
            return Ok(duplicatesFinder.FindDuplicates(dto.Path, dto.MaxDepth, filter));
        }
    }
}
