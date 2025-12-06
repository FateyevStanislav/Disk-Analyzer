namespace DiskAnalyzer.Domain.Models;

public record FilterInfo(
    string Type,
    Dictionary<string, string> Parameters);