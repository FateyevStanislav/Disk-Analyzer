using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace DiskAnalyzer.Api.Controllers;

[Route("api/history")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IRepository repository;

    public HistoryController(IRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Получить всю историю результатов анализа
    /// </summary>
    /// <param name="descending">true - от новых к старым, false - от старых к новым (по умолчанию)</param>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool descending = false)
    {
        var results = await repository.GetAllOrderedAsync(descending);
        return Ok(results);
    }

    /// <summary>
    /// Получить конкретный результат по ID
    /// </summary>
    /// <param name="id">Идентификатор результата</param>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await repository.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Добавить новый результат измерений в историю
    /// </summary>
    /// <param name="result">Результат измерения</param>
    [HttpPost("append")]
    public async Task<IActionResult> Append([FromBody] AnalysisResult result)
    {
        try
        {
            await repository.AddAsync(result);
            return Ok();
        }

        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Удалить результат из истории
    /// </summary>
    /// <param name="id">Идентификатор результата</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await repository.RemoveAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Очистить всю историю
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await repository.ClearAsync();
        return NoContent();
    }

    /// <summary>
    /// Получить количество записей в истории
    /// </summary>
    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        var count = await repository.CountAsync();
        return Ok(new { count });
    }
}
