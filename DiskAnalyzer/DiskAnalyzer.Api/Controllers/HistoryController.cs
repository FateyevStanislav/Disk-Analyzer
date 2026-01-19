using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

[Route("api/history")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IRepository<AnalysisResult> _repository;

    public HistoryController(IRepository<AnalysisResult> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool descending = false)
    {
        var results = await _repository.GetAllOrderedAsync(descending);
        return Ok(results);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("append")]
    public async Task<IActionResult> Append([FromBody] AnalysisResult result)
    {
        await _repository.AddAsync(result);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _repository.RemoveAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _repository.ClearAsync();
        return NoContent();
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        var count = await _repository.CountAsync();
        return Ok(new { count });
    }
}
