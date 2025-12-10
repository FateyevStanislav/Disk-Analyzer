namespace DiskAnalyzer.Domain.Records.Grouping;

public record Group(string Key, IReadOnlyList<FileDetails> Files);