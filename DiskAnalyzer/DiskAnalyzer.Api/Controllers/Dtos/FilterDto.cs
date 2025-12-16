namespace DiskAnalyzer.Api.Controllers.Dtos;

public record FilterDto(string Type, Dictionary<string, string> FilterParams);
