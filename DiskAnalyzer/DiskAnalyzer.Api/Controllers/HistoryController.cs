using DiskAnalyzer.Api.Modules;
using DiskAnalyzer.Domain.Records;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        public static History history = new();

        [HttpGet]
        [HttpGet("{countOfRecords:int}")]
        public IActionResult Get(int countOfRecords = 0)
        {
            if (countOfRecords == 0) {
                return Ok(history.GetAllRecords());
            }

            else
            {
                return Ok(history.GetLastRecords(countOfRecords));
            }
        }
    }
}
