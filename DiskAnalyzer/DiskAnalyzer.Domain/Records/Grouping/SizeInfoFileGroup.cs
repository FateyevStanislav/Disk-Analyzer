namespace DiskAnalyzer.Domain.Records.Grouping;

public record SizeInfoFileGroup(
    string Key,
    long TotalSize,
    IReadOnlyList<FileDetails> Files) : Group(Key, Files);