using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private static readonly List<Guid> History = new();

        public static void AddIdToHistory(Guid id)
        {
            History.Add(id);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var repo = new ConcDictRepository();
            var result = new List<WeightingRecord>();
            foreach (var id in History)
            {
                result.Add(repo.Get(id));
            }

            return Ok(result);
        }
    }
}
    