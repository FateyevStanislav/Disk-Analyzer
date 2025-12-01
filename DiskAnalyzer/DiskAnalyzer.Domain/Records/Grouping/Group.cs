namespace DiskAnalyzer.Domain.Records.Grouping;

public abstract record Group(string Key, IReadOnlyList<FileDetails> Files);