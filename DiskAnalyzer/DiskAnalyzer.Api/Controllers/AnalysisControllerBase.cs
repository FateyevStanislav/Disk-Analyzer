using DiskAnalyzer.Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiskAnalyzer.Api.Controllers;

public abstract class AnalysisControllerBase : ControllerBase
{
    protected IActionResult OkAnalysis(AnalysisResult result)
    {
        var json = JsonSerializer.Serialize(result);
        return Content(json, "application/json");
    }
}
