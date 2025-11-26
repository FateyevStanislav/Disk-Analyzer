using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private static readonly ConcDictRepository History = new();

        public static void AddIdToHistory(DirectoryMeasurementRecord record)
        {
            History.Add(record);
        }

        [HttpGet]
        [HttpGet("{countOfRecords:int}")]
        public IActionResult Get(int countOfRecords = 0)
        {
            var repo = new ConcDictRepository();
            var result = new List<DirectoryMeasurementRecord>();
            var i = 0;

            foreach (var record in History.GetAllAscOrder())
            {
                if (countOfRecords > 0 && i >= countOfRecords)
                {
                    break;
                }

                result.Add(record);
                i++;
            }

            return Ok(result);
        }
    }
}
