using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiskAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/measurements/duplicates")]
    public class DuplicateFinderController : AnalysisControllerBase
    {
        private readonly IDuplicatesFinder duplicatesFinder;
        private readonly IRepository repository;

        public DuplicateFinderController(IDuplicatesFinder duplicatesFinder, IRepository repository)
        {
            this.duplicatesFinder = duplicatesFinder;
            this.repository = repository;
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
