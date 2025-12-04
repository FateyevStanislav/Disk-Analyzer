namespace DiskAnalyzer.Api.Controllers;

public record FilterDto(string Type, Dictionary<string, string> FilterParams);
