using DiskAnalyzer.Api.Modules;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/requestInfo")]
    public class RequestInfoController : ControllerBase
    {
        [HttpGet("filters")]
        public IActionResult GetFilters()
        {
            return Ok(ApiReflection.GetFiltersData());
        }
    }
}
