using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/measurements/duplicates")]
    public class DuplicateFinderController : AnalysisControllerBase
    {
        private readonly IDuplicatesFinder duplicatesFinder;

        public DuplicateFinderController(IDuplicatesFinder duplicatesFinder)
        {
            this.duplicatesFinder = duplicatesFinder;
        }

        [HttpPost]
        public async Task<IActionResult> Make(DuplicateFinderDto dto)
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
