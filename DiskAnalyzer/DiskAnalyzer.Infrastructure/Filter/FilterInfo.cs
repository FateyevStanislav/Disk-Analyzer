namespace DiskAnalyzer.Infrastructure.Filter;

public record FilterInfo(
    string Type,
    Dictionary<string, string> Parameters);